
namespace Bets.Abstractions.Client.Exceptions
{
    [Serializable]
    public class ClientBaseException : Exception
    {
        public ClientBaseException()
        {

        }

        public ClientBaseException(string message) : base(message)
        {

        }

        public ClientBaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
