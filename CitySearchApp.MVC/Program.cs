using CitySearchApp.Application.Contracts.Persistance;
using CitySearchApp.MVC.Contracts;
using CitySearchApp.MVC.Repositories;
using CitySearchApp.MVC.Services;
using CitySearchApp.MVC.Services.Base;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddHttpClient<IClient, Client>(cl => cl.BaseAddress = new Uri("https://localhost:7146"));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<ICityCZService, CityCZService>();
builder.Services.AddScoped<CitySearchApp.MVC.Repositories.ICityCZRepository, CitySearchApp.MVC.Repositories.CityCZRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
