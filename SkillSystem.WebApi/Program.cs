using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

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