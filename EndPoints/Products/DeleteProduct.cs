using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniValidation;
using Shop.Data;

namespace Shop.EndPoints.Products
{
    public class DeleteProduct
    {
        public static string Route => "/products/{id}";
        [Authorize (Roles = "manager")]
        public static async Task<IResult> Action ([FromRoute] int id, [FromServices] DataContext context)
        {
            if(!MiniValidator.TryValidate(id,out var errors))
                return Results.ValidationProblem(errors);

            var category = await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
            if(category == null)
                return Results.NotFound("a categoria não foi encontrada");
            
            try
            {
                context.Categories.Remove(category);
                await context.SaveChangesAsync();
                return Results.Accepted($"{id} deletado", id);    
            }
            catch
            {
                return Results.BadRequest("Não foi possível remover a categoria");
            }
            
        }
    }
}