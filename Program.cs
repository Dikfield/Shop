using Shop.Data;
using Microsoft.EntityFrameworkCore;
using Shop.EndPoints.Categories;
using Shop.EndPoints.Products;
using System.Text;
using Shop;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shop.EndPoints.Controller;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.OpenApi.Models;
using Shop.EndPoints.HomeController;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddResponseCompression(options =>
{
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = 
        ResponseCompressionDefaults.MimeTypes.Concat(new[] {"application/json"});
    
});

builder.Services.AddResponseCaching();


builder.Services.AddControllers();


var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthentication(x =>
{
x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("Database"));
//builder.Services.AddSqlServer<DataContext>(builder.Configuration["ConnectionString:connectionString"]);




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>
{
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "Shop Api", Version = "v1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json","Shop API V1");
    });
}

app.UseHttpsRedirection();

app.MapPost(Post.Route, Post.Action);
app.MapGet(GetById.Route, GetById.Action);
app.MapGet(Get.Route, Get.Action);
app.MapPut(Put.Route, Put.Action);
app.MapDelete(Delete.Route, Delete.Action);

app.MapPost(PostProduct.Route, PostProduct.Action);
app.MapGet(GetProducts.Route, GetProducts.Action);
app.MapGet(GetProductById.Route, GetProductById.Action);
app.MapGet(GetByCategoryId.Route, GetByCategoryId.Action);
app.MapPut(PutProduct.Route, PutProduct.Action);
app.MapDelete(DeleteProduct.Route, DeleteProduct.Action);
app.MapPost(UserLogin.Route, UserLogin.Action);
app.MapPost(UserPost.Route, UserPost.Action);
app.MapGet(HomeGet.Route, HomeGet.Action);
app.MapGet(GetManager.Route, GetManager.Action);

app.UseCors(x=>x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
