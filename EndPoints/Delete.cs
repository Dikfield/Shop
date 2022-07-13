using Microsoft.AspNetCore.Mvc;

namespace Shop.EndPoints
{
    public class Delete
    {
        public static string Route => "/categories/{id}";
        public static async Task<IResult> Action ()
        {
            return Results.Ok("Delete");
        }
    }
}