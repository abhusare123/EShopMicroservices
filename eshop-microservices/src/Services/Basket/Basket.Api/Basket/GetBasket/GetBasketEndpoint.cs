

namespace Basket.Api.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(username));
            return Results.Ok(result.Adapt<GetBasketResponse>());
        })
        .WithDisplayName("GetBasket");
    }
}
