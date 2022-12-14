using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SkillSystem.Application;
using SkillSystem.Application.Common.Services;
using SkillSystem.Infrastructure;
using SkillSystem.Infrastructure.Persistence;
using SkillSystem.WebApi.Configuration;
using SkillSystem.WebApi.Middlewares;
using SkillSystem.WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options => options.AddServerHeader = false);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ICurrentUserProvider, CurrentUserProvider>();

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
builder.Services.ConfigureOptions<SwaggerGenSetup>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        options =>
        {
            options.OAuthClientId("skill-system-swagger");
            options.OAuthScopes("SkillSystem.WebApi");
            options.OAuthUsePkce();
        }
    );
}

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<SkillSystemDbInitializer>();
    await dbInitializer.InitializeAsync();
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