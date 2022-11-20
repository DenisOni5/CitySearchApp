using CitySearchApp.Application;
using CitySearchApp.Persistance;
using CitySearchApp.Persistance.Seed;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistanceServices(builder.Configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CitySearchApp.Api v1"));
}

DatabaseSeedMigration.EnsurePopulated(app);

app.Use(async (context, next) =>
{
    context.RequestServices.GetRequiredService<ICitySeed>().Seed();
    await next(context);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
