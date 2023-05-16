using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalesPageAPI.Authorization;
using SalesPageAPI.Data;
using SalesPageAPI.Models;
using SalesPageAPI.Services.SalesListServices;
using SalesPageAPI.Services.UserServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Create a database context with MySql 
// this value is accessed through the secrets
var connectionString = builder.Configuration["ConnectionStrings:UserConnection"];


builder.Services.AddDbContext<UserDbContext>
    (
    opts =>
    {
        opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }
    );

builder.Services.AddDbContext<SalesListDbContext>
    (
    opts =>
    { opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); }
    );

// Create a "Identity" with the User class and save it in the database as soon as UserDbContext class 
builder.Services
    .AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();


// Configure the AutoMapper 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());



// Configure the Dependency Injection for services: User register, User login
// token and SalesList
builder.Services.AddScoped<UserRegisterService>();
builder.Services.AddScoped<UserLoginService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<SalesListServices>();

// Configure application encryption
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.GetTempPath()));


// Integrate the access policy to the application
builder.Services.AddSingleton<IAuthorizationHandler, AgeAuthorization>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add the schema for authentication for users 
builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.TokenValidationParameters = new
    TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,

        // this value is accessed through the secrets
        IssuerSigningKey = new
        SymmetricSecurityKey(Encoding.UTF8.GetBytes("GEOQGNQOGINEQOUGFQWEFHF")),

        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };

});

// Create a Policy for use of content with base in MinimumAgeRequiredPolicy class 
builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy("IdadeNecessariaMinima", policy =>
        policy.AddRequirements(new MinimumAgeRequiredPolicy(21))
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();


app.Run();
