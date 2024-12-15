using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MerryChristmas.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDb"));
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var settings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>();
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", async context =>
{
    context.Response.Redirect("/index.html");
});

app.MapControllers();
app.Run();