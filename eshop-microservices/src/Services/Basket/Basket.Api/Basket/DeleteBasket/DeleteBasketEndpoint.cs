
namespace Basket.Api.Basket.DeleteBasket;

//public record DeleteBasketRequest(string Username);
public record DeleteBasketResponse(bool IsDeleted);

public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{username}", async (string username, ISender sender) =>
        {
            var result = await sender.Send(new DeleteBasketCommand(username));
            return Results.Ok(result);
        });
    }
}
