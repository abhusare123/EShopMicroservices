namespace Catalog.Api.Products.GetProductByCategory
{
    public class GetProductByCategoryEndpoint : ICarterModule
    {

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}",
                async (string category, ISender sender) =>
                {
                    var result = await sender.Send(new GetProductByCategoryQuery(category));
                    var response = result.Adapt<GetProductByCategoryResult>();
                    return Results.Ok(response);
                });
        }
    }
}
