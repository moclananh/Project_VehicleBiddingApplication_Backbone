using System.Text.Json;
using System.Text.Json.Serialization;

namespace BiddingApp.BuildingBlock.Extentions
{
    public class StringEnumConverter<TEnum> : JsonConverter<TEnum> where TEnum : struct, Enum
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }
            throw new JsonException($"Invalid value '{value}' for enum '{typeof(TEnum).Name}'");
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
