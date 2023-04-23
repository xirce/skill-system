using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Refit;
using SkillSystem.Application;
using SkillSystem.Application.Common.Services;
using SkillSystem.IdentityServer4.Client;
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
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(
        options =>
        {
            options.Audience = "SkillSystem.WebApi";
            options.Authority = builder.Configuration.GetSection(nameof(SkillSystemWebApiSettings))
                .Get<SkillSystemWebApiSettings>().IdentityBaseUrl;
            options.RequireHttpsMetadata = false;
        });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureOptions<SwaggerGenSetup>();
builder.Services.ConfigureOptions<SwaggerUiSetup>();

builder.Services.AddRefitClient<IUsersClient>()
    .ConfigureHttpClient(
        httpClient => httpClient.BaseAddress = new Uri(
            builder.Configuration.GetSection(nameof(SkillSystemWebApiSettings)).Get<SkillSystemWebApiSettings>()
                .IdentityApiBaseUrl));

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<SkillSystemDbInitializer>();
    await dbInitializer.InitializeAsync();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(
    options => options.WithOrigins(
            app.Configuration.GetSection(nameof(SkillSystemWebApiSettings)).Get<SkillSystemWebApiSettings>().WebAppUrl)
        .AllowAnyHeader()
        .AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
