
namespace Bets.Abstractions.Client.Serialization
{
    public class JsonString
    {
        public string Value { get; }

        public JsonString(string value) => Value = value;
    }
}
