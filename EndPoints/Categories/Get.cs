using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
using Microsoft.EntityFrameworkCore;

namespace Shop.EndPoints.Categories
{
    public class Get
    {
        public static string Route => "/categories";
        public static async Task<IResult> Action ([FromServices]DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();

            return Results.Ok(categories);
        }
    }
}