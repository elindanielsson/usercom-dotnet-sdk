using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace UserCom.Http
{
    public class HttpQueryStringBuilder
    {
        private readonly List<KeyValuePair<string, string>> _inner = new List<KeyValuePair<string, string>>();

        public void Add(string key, string value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException(nameof(value));
            }

            _inner.Add(new KeyValuePair<string, string>(key, value));
        }

        public void Add(string key, bool value) => Add(key, value ? "true" : "false");

        public void Add<T>(string key, T value) where T : IConvertible => Add(key, (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture));

        public QueryString BuildQuery(bool sortKeys = true, string? collateKeysBy = null)
        {
            IEnumerable<KeyValuePair<string, string>> items = _inner;

            if (!string.IsNullOrEmpty(collateKeysBy))
            {
                items = items.GroupBy(i => i.Key, e => e.Value)
                    .Select(g => new KeyValuePair<string, string>(g.Key, string.Join(collateKeysBy, g)));
            }

            if (sortKeys)
            {
                items = items.OrderBy(i => i.Key, StringComparer.OrdinalIgnoreCase);
            }

            return new QueryString(items.ToArray());
        }
    }
}