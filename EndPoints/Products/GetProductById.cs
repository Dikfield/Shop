using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace Shop.EndPoints.Products
{
    public class GetProductById
    {
        public static string Route => "/products/{id:int}";
        public static async Task<IResult> Action ([FromRoute] int id,[FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(id,out var errors))
                return Results.ValidationProblem(errors);
            
            var product = await context
            .Products
            .Include(x=>x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(a=>a.Id == id);

            if(product == null)
                return Results.NotFound();
            return Results.Ok($"Get {product.Id}");
        }
    }
}