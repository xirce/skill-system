using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SkillSystem.Application;
using SkillSystem.Infrastructure;
using SkillSystem.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

builder.Services.AddControllers()
    .AddJsonOptions(
        options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.Audience = "SkillSystem.WebApi";
            options.Authority = "https://localhost:5001";
            options.RequireHttpsMetadata = false;
        }
    );
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(
    options => options.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();