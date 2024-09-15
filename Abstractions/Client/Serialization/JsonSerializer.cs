using System.Text.Json;

namespace Client.Serialization
{
    public class JsonSerializer
    {
        private readonly JsonSerializerOptions _options;

        public JsonSerializer(JsonSerializerOptions options)
        {
            _options = options;
        }

        public string Serialize<T>(T value) where T : class
        {
            return System.Text.Json.JsonSerializer.Serialize(value, _options);
        }

        public T Deserialize<T>(string json) where T : class
        {
            return System.Text.Json.JsonSerializer.Deserialize<T>(json, _options);
        }
    }
}
