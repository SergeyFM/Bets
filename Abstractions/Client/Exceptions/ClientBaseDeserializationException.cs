
namespace Client.Exceptions
{
    [Serializable]
    public class ClientBaseDeserializationException : ClientBaseException
    {
        public ClientBaseDeserializationException()
        {

        }

        public ClientBaseDeserializationException(string message) : base(message)
        {

        }

        public ClientBaseDeserializationException(string message, Exception innerException) : base(message, innerException)
        {

        }

        public string JsonContent { get; internal set; }
    }
}
