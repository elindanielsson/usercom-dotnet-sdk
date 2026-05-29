using System;
using Newtonsoft.Json;

namespace UserCom.Serialization
{
    public class StringValueMaxLengthConverter : JsonConverter
    {
        private readonly int _maxLength;

        public StringValueMaxLengthConverter(int maxLength)
        {
            _maxLength = maxLength;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            var strValue = value?.ToString();

            if (!string.IsNullOrEmpty(strValue) && strValue.Length > _maxLength)
            {
                var substring = $"{strValue.Substring(0, _maxLength - 3)}...";
                
                serializer.Serialize(writer, substring);
            }
            else
            {
                serializer.Serialize(writer, strValue);
            }
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override bool CanRead => false;
        public override bool CanWrite => true;
    }
}