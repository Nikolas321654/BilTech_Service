using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var jwtSecretKey = builder.Configuration["JWT_SECRET_KEY"];
if (string.IsNullOrEmpty(jwtSecretKey))
    throw new Exception("JWT_SECRET_KEY is not configured in environment variables!");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes($"{builder.Configuration["JWT_SECRET_KEY"]}")),
            ValidateIssuer = false,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("WarehouseWorkerOnly", policy =>
        policy.RequireRole("WarehouseWorker", "Owner"));

    options.AddPolicy("ShopWorkerOnly", policy =>
        policy.RequireRole("ShopWorker", "Owner"));

    options.AddPolicy("AccountantOnly", policy =>
        policy.RequireRole("Accountant", "Owner"));
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
//app.UseHttpsRedirection();
app.MapReverseProxy();
app.Run();