using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
using Microsoft.EntityFrameworkCore;
using Shop.Services;

namespace Shop.EndPoints.Controller
{
      public class UserLogin
    {
        public static string Route => "users/login";
       
        public static async Task<IResult> Action ([FromServices] DataContext context, [FromBody] User model)
        {
            var user = await context.Users
                .AsNoTracking()
                .Where(x=>x.Username == model.Username && x.Password == model.Password)
                .FirstOrDefaultAsync();

                if(user == null)
                    return Results.NotFound("Usuário ou senha inválidos");

                var token = TokenService.GenerateToken(user);
                return Results.Ok(new {user = user, token = token});
        }
    }
}  
