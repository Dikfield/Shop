using Microsoft.AspNetCore.Mvc;
using Shop.Models;

namespace Shop.EndPoints
{
    public class Get
    {
        public static string Route => "/categories";
        public static async Task<ActionResult<List<Category>>> Action ()
        {
            return new List<Category>();
        }
    }
}