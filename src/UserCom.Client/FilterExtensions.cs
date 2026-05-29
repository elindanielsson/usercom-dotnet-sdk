using System.Collections.Generic;
using UserCom.Model.Users;

namespace UserCom
{
    public static class FilterExtensions
    {
        private static readonly Dictionary<CustomAttributeLookup, string> CustomAttributeLookupSuffixes = new Dictionary<CustomAttributeLookup, string>
        {
            [CustomAttributeLookup.Contains] = "__contains",
            [CustomAttributeLookup.ContainsCaseInsensitive] = "__icontains",
            [CustomAttributeLookup.EndsWith] = "__endswith",
            [CustomAttributeLookup.EndsWithCaseInsensitive] = "__iendswith",
            [CustomAttributeLookup.StartsWith] = "__startswith",
            [CustomAttributeLookup.StartsWithCaseInsensitive] = "__istartswith",
            [CustomAttributeLookup.GreaterThan] = "__gt",
            [CustomAttributeLookup.GreaterOrEqualThan] = "__gte",
            [CustomAttributeLookup.LessThan] = "__lt",
            [CustomAttributeLookup.LessOrEqualThan] = "__lte"
        };

        public static (string key, string value) ToQueryParam(this CustomAttributeFilter filter)
        {
            return ($"{filter.Name}{CustomAttributeLookupSuffixes[filter.Lookup]}", filter.Value.ToString());
        }

        public static (string key, string value) ToQueryParam(this UserFilter filter)
        {
            return (filter.Lookup switch
            {
                UserLookup.Contains => $"{filter.Name}__contains",
                UserLookup.GreaterThan => $"{filter.Name}__gt",
                UserLookup.GreaterOrEqualThan => $"{filter.Name}_gte",
                UserLookup.LessThan => $"{filter.Name}__lt",
                UserLookup.LessOrEqualThan => $"{filter.Name}__lte"
            }, filter.Value.ToString());
        }
    }
}