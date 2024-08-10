using Basket.Api.Data;
using BuildingBlocks.CQRS;
using FluentValidation;

namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string Username) : ICommand<DeleteBasketResult>;

public record DeleteBasketResult(bool IsDeleted);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
    }
}

public class DeleteBasketHandler(IBasketRepository basketRepository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
{
    public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        await basketRepository.DeleteBasket(request.Username, cancellationToken);
        return new DeleteBasketResult(true);
    }
}

