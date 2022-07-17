using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
using Microsoft.EntityFrameworkCore;

namespace Shop.EndPoints.Controller
{
      public class GetManager
      {
        public static string Route => "users";
        [Authorize(Roles = "manager")]
        public static async Task<IResult> Action ([FromServices] DataContext context, [FromBody] User model)
        {
            var users = await context
                .Users
                .AsNoTracking()
                .ToListAsync();
            return Results.Ok(users);
        }
    }
}  
