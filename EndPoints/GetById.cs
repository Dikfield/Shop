using Microsoft.AspNetCore.Mvc;

namespace Shop.EndPoints
{
    public class GetById
    {
        public static string Route => "/categories/{id:int}";
        public static async Task<IResult> Action ([FromRoute] int id)
        {
            return Results.Ok($"Get {id}");
        }
    }
}