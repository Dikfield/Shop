using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Shop.EndPoints.Products
{
    public class GetProducts
    {
        public static string Route => "/products";
        [AllowAnonymous]
        public static async Task<IResult> Action ([FromServices]DataContext context)
        {
            var products = await context.Products
            .Include(x=>x.Category)
            .AsNoTracking()
            .ToListAsync();

            return Results.Ok(products);
        }
    }
}