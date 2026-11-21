using CatalogoProdutos.Api.Auth;
using CatalogoProdutos.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<CatalogoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Catalogo")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirFrontend", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Basic",
    new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic"
    });
    c.AddSecurityRequirement(doc =>
        new OpenApiSecurityRequirement
        {
            [new OpenApiSecuritySchemeReference(
        "Basic", doc)] = []
    });
});

builder.Services.AddControllers();

builder.Services.AddAuthentication("Basic")
    .AddScheme<Microsoft.AspNetCore.Authentication.AuthenticationSchemeOptions,
    BasicAuthHandler>("Basic", null);

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("PermitirFrontend");
app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();