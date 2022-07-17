using Microsoft.AspNetCore.Mvc;
using Shop.Data;
using Shop.Models;

namespace Shop.EndPoints.HomeController
{
    public class HomeGet
    {
        public static string Route => "/v1";
              
        
        public static async Task<IResult> Action ([FromServices]DataContext context)
        {
            var employee = new User{Id =1, Username = "robin", Password = "robin", role = "employee"};
            var manager = new User{Id =2, Username = "batman", Password = "batman", role = "manager"};
            var category = new Category{Id =1, Title = "Informática"};
            var product = new Product{Id =1, Category = category, Title = "mouse", price = 299, Description = "Mouse Gamer" };
            context.Users.Add(employee);
            context.Users.Add(manager);
            context.Categories.Add(category);
            context.Products.Add(product);

            await context.SaveChangesAsync();

            return Results.Ok("Dados configurados");
        }
    }
}