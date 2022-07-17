using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using MiniValidation;
using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Shop.EndPoints.Categories
{
    public class Put
    {
        public static string Route => "/categories/{id:int}";
        [Authorize (Roles = "employee")]
        public static async Task <IResult> Action (int id, [FromBody]Category model,[FromServices] DataContext context)
        {
            if(model.Id !=id)
                return Results.NotFound("categoria não encontrada");

            if(!MiniValidator.TryValidate(id, out var errors))
                return Results.BadRequest(errors);
            
            try
            {
                context.Entry<Category>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Results.Accepted($" ok updated {model}", model);
            }
            catch (DbUpdateConcurrencyException)
            {
                return Results.BadRequest("Esse registro já foi atualizado");
            }
            catch (Exception)
            {
                return Results.BadRequest("Não foi possível atualizar o banco");
            }
        }
    }
}