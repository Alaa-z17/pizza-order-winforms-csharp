using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PizzaOrderSystem.Models;

namespace PizzaOrderSystem.Services
{
    public class PolymorphicBaseItemConverter : JsonConverter<BaseItem>
    {
        public override BaseItem? Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);
            JsonElement root = doc.RootElement;

            if (!root.TryGetProperty("TypeDiscriminator", out JsonElement discriminator))
                throw new JsonException("Missing TypeDiscriminator");

            string typeName = discriminator.GetString() ?? string.Empty;

            return typeName switch
            {
                nameof(Pizza) => JsonSerializer.Deserialize<Pizza>(root.GetRawText(), options),
                nameof(Drink) => JsonSerializer.Deserialize<Drink>(root.GetRawText(), options),
                nameof(Side) => JsonSerializer.Deserialize<Side>(root.GetRawText(), options),
                _ => throw new NotSupportedException($"Unknown type: {typeName}")
            };
        }

        public override void Write(Utf8JsonWriter writer, BaseItem value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("TypeDiscriminator", value.GetType().Name);
            var json = JsonSerializer.Serialize(value, value.GetType(), options);
            using JsonDocument doc = JsonDocument.Parse(json);
            foreach (var prop in doc.RootElement.EnumerateObject())
                prop.WriteTo(writer);
            writer.WriteEndObject();
        }
    }
}