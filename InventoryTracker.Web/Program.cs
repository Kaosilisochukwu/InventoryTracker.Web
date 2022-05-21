using InventoryTracker.Data;
using InventoryTracker.Data.Interfaces;
using InventoryTracker.Data.Services;
using InventoryTracker.Domain.Maps;
using InventoryTracker.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddServerSideBlazor();
builder.Services.AddDbContext<InventoryDbContext>(option => option.UseSqlite(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInventoryTransactionService, InventoryTransactionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
