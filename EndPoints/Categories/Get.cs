using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Shop.EndPoints.Categories
{
    public class Get
    {
        public static string Route => "/categories";
        [AllowAnonymous]
        [ResponseCache(VaryByHeader ="User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)]
        //[ResponseCache(Duration =0, Location = ResponseCacheLocation.None,NoStore = true)]
        public static async Task<IResult> Action ([FromServices]DataContext context)
        {
            var categories = await context.Categories.AsNoTracking().ToListAsync();

            return Results.Ok(categories);
        }
    }
}