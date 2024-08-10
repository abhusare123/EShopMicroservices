using Basket.Api.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{ 
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty().WithMessage("UserName is required");
    }
    
}

public class StoreBasketCommandHandler(IBasketRepository basketRepository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand request, CancellationToken cancellationToken)
    {
        var cart = request.ShoppingCart;
        var username = await basketRepository.StoreBasketAsync(cart, cancellationToken);
        return new StoreBasketResult("Test-user");
    }
}
