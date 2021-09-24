using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services.Helpers
{
    public class JsonUnixTimeConverter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.Integer)
                throw new Exception("Unexpected token type.");

            var unixTime = (long)reader.Value;

            return UnixTimeHelper.ToDateTime(unixTime);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (false == value is DateTime)
            {
                throw new Exception("Unexpected object type.");
            }

            var dateTime = (DateTime)value;

            var unixTime = UnixTimeHelper.ToUnixTime(dateTime);

            writer.WriteValue(unixTime);
        }
    }
}