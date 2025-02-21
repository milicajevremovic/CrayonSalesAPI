using Crayon.Infrastructure.Data;
using Crayon.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddApplicationPart(typeof(Program).Assembly)
    .AddControllersAsServices();

builder.Services.AddEndpointsApiExplorer(); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Crayon Sales API", Version = "v1" });
});

// MediatR scans the current assembly and other assemblies
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

// EF Core – here using InMemoryDatabase for demonstration
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("CrayonCloudSalesDb"));

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IPurchasedSoftwareRepository, PurchasedSoftwareRepository>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Crayon API Portal");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
