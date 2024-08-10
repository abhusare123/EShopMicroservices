using BuildingBlocks.Exeptions;
using System.Runtime.Serialization;

namespace Basket.Api.Exception;

public class BasketNotFoundException : NotFoundException
{
    public BasketNotFoundException(string userName) : base("Basket", userName)
    {
    }
}