using BuildingBlocks.Exeptions;
using System.Runtime.Serialization;

namespace Catalog.Api.Exceptions
{
    [Serializable]
    internal class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid id) : base("Product", id)
        {
        }
    }
}