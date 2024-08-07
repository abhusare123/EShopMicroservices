using System.Runtime.Serialization;

namespace Catalog.Api.Exceptions
{
    [Serializable]
    internal class ProductNotFoundException : Exception
    {
        private Guid id;

        public ProductNotFoundException()
        {
        }

        public ProductNotFoundException(Guid id)
        {
            this.id = id;
        }

        public ProductNotFoundException(string? message) : base(message)
        {
        }

        public ProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected ProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}