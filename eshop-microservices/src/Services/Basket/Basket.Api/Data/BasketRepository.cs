using Basket.Api.Exception;

namespace Basket.Api.Data;

public class BasketRepository(IDocumentSession documentSession) : IBasketRepository
{

    public async Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken)
    {
        documentSession.Delete<ShoppingCart>(userName);
        await documentSession.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName, CancellationToken cancellationToken)
    {
        var basket= await documentSession.LoadAsync<ShoppingCart>(userName, cancellationToken);
        return basket ?? throw new BasketNotFoundException(userName);
    }

    public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken cancellationToken)
    {
        documentSession.Store(basket);
        await documentSession.SaveChangesAsync(cancellationToken);
        return basket;
    }
}
