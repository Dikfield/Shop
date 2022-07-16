using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using MiniValidation;
using Shop.Data;

namespace Shop.EndPoints.Products
{
    public class PostProduct
    {
        public static string Route => "/products";
        public static async Task<IResult> Action ([FromBody] Product product, [FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(product, out var errors))
                return Results.BadRequest(errors);
            try
            {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return Results.Created($"/categories {product}", product);
            }
            catch
            {
                return Results.BadRequest("Não foi possível criar a categoria");
            }
        }
    }
}