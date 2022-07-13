using Shop.EndPoints;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost(Post.Route, Post.Action);
app.MapGet(GetById.Route, GetById.Action);
app.MapGet(Get.Route, Get.Action);
app.MapPut(Put.Route, Put.Action);
app.MapDelete(Delete.Route, Delete.Action);

app.UseAuthorization();

app.MapControllers();

app.Run();
