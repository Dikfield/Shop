using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;
using MiniValidation;
namespace Shop.EndPoints.Controller
{
      public class UserPost
    {
        public static string Route => "users";
        [AllowAnonymous]
        public static async Task<IResult> Action ([FromServices] DataContext context, [FromBody] User model)
        {
            if(!MiniValidator.TryValidate(model, out var errors))
                return Results.BadRequest();

            try
            {
                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Results.Created($" user criado {model}", model);
            }
            catch(Exception)
            {
                return Results.BadRequest("Não foi possível criar o usuário");
            }
        }
    }
}  
