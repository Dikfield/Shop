using Microsoft.AspNetCore.Mvc;
using MiniValidation;
using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Shop.EndPoints.Categories
{
    public class GetById
    {
        public static string Route => "/categories/{id:int}";
        [AllowAnonymous]
        public static async Task<IResult> Action ([FromRoute] int id,[FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(id,out var errors))
                return Results.ValidationProblem(errors);
            
            var category = await context.Categories.AsNoTracking().FirstOrDefaultAsync(a=>a.Id == id);
            if(category == null)
                return Results.NotFound();
            return Results.Ok($"Get {category.Id}");
        }
    }
}