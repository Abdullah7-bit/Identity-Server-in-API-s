using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config Database
builder.Services
    .AddDbContext<IdentityDbContext>(options => options.UseInMemoryDatabase("AnyDB"));

// add auth scheme
builder.Services
    .AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services
    .AddAuthorizationBuilder();

// add identity and EF config
builder.Services
    .AddIdentityCore<IdentityUser>()
    .AddEntityFrameworkStores<IdentityDbContext>()
    .AddApiEndpoints();




var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
