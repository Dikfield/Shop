using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using Shop.Data;
using Microsoft.EntityFrameworkCore;

namespace Shop.EndPoints.Products
{
    public class GetByCategoryId
    {
        public static string Route => "/products/category/{id:int}";
        public static async Task<IResult> Action ([FromRoute] int id,[FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(id,out var errors))
                return Results.ValidationProblem(errors);
            
            var products = await context
            .Products
            .Include(x=>x.Category)
            .AsNoTracking()
            .Where(x=>x.CategoryId == id)
            .ToListAsync();
            
            if(products == null)
                return Results.NotFound();
            return Results.Ok($"Get {products}");
        }
    }
}