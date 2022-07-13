using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.EndPoints
{
    public class Put
    {
        public static string Route => "/categories/{id:int}";
        public static async Task <ActionResult<Category>> Action (int id, [FromBody]Category model)
        {
            if(model.Id ==id)
                return model;
            return null;
        }
    }
}