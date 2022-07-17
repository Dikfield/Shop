using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using MiniValidation;
using Shop.Data;
using Microsoft.AspNetCore.Authorization;

namespace Shop.EndPoints.Categories
{
    public class Post
    {
        public static string Route => "/categories";
        [Authorize (Roles = "employee")]
        public static async Task<IResult> Action ([FromBody] Category category, [FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(category, out var errors))
                return Results.BadRequest(errors);
            try
            {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return Results.Created($"/categories {category.Id}", category);
            }
            catch
            {
                return Results.BadRequest("Não foi possível criar a categoria");
            }
        }
    }
}