using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace UserCom.Serialization
{
    public class AttributeValueConverter : JsonConverter
    {
        private const string ArrayRegex = @"^\[.*\]$";
        private const string DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var strValue = value.ToString();
            if (bool.TryParse(strValue, out var boolValue))
            {
                serializer.Serialize(writer, boolValue);
            }
            else if (int.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var intValue))
            {
                serializer.Serialize(writer, intValue);
            }
            else if (float.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var floatValue))
            {
                serializer.Serialize(writer, floatValue);
            }
            else if (double.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleValue))
            {
                serializer.Serialize(writer, doubleValue);
            }
            else if (decimal.TryParse(strValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalValue))
            {
                serializer.Serialize(writer, decimalValue);
            }
            else if (Regex.IsMatch(strValue, ArrayRegex))
            {
                var arrayStr = strValue.Substring(1, strValue.Length - 2);
                var arrayValue = arrayStr.Split(',').Select(s => s.Trim().Trim('"'));

                serializer.Serialize(writer, arrayValue);
            }
            else
            {
                serializer.Serialize(writer, value);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value;

            if (value is DateTime dateTimeValue)
            {
                return dateTimeValue.ToString(DateTimeFormat, CultureInfo.InvariantCulture);
            }

            if (value is bool boolValue)
            {
                return boolValue.ToString(CultureInfo.InvariantCulture).ToLowerInvariant();
            }

            if (value is float floatValue)
            {
                return floatValue.ToString(CultureInfo.InvariantCulture);
            }

            if (value is decimal decimalValue)
            {
                return decimalValue.ToString(CultureInfo.InvariantCulture);
            }

            if (value is double doubleValue)
            {
                return doubleValue.ToString(CultureInfo.InvariantCulture);
            }

            if (value is int intValue)
            {
                return intValue.ToString(CultureInfo.InvariantCulture);
            }

            try
            {
                var arrayValue = serializer.Deserialize<string[]>(reader);

                return $"[{string.Join(",", arrayValue.Select(s => $"\"{s}\""))}]";
            }
            catch { }

            return value.ToString();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}