using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.EndPoints
{
    public class Post
    {
        public static string Route => "/categories";
        public static async Task<ActionResult<Category>> Action ([FromBody] Category category)
        {
            return category;
        }
    }
}