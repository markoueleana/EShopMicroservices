﻿
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategoryId
{
    //public record GetProductByCategoryEndpointRequest();

    public record GetProductByCategoryEndpointResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/category/{category}",
               async (string category, ISender sender) =>
            {
               var result = await sender.Send(new GetProductByCategoryQuery(category));

               var response = result.Adapt<GetProductByCategoryEndpointResponse>();

               return Results.Ok(response); 
           })
        .WithName("GetProductByCategory")
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Category")
        .WithDescription("Get Product By Category");
        }
    }
}
