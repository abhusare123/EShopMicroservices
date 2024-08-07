
namespace Catalog.Api.Products.GetProducts;

public record GetProductsQuery : IQuery<GetProductsResult>;
public record GetProductsResult(IEnumerable<Product> Products);

public class GetProductsHandler(IDocumentSession documentSession) : IQueryHandler<GetProductsQuery, GetProductsResult>
{
    public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        var products = documentSession.Query<Product>().ToList();
        return new GetProductsResult(products);
    }
}
