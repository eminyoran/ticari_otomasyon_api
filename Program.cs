using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using OtomasyonApi.Data;
using OtomasyonApi.Repositories;
using OtomasyonApi.Services;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------
// DATABASE
// --------------------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<DapperContext>();

// --------------------------------------
// REPOSITORIES & SERVICES
// --------------------------------------
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICariService, CariService>();
builder.Services.AddScoped<ICariHareketService, CariHareketService>();
builder.Services.AddScoped<ICariHareketRepository, CariHareketRepository>();
builder.Services.AddScoped<IUrunService, UrunService>();
builder.Services.AddScoped<IFaturaService, FaturaService>();
builder.Services.AddScoped<IKasaService, KasaService>();
builder.Services.AddScoped<IBankaService, BankaService>();


// --------------------------------------
// CONTROLLERS
// --------------------------------------
builder.Services.AddControllers();

// --------------------------------------
// CORS (FLUTTER WEB İÇİN ZORUNLU)
// --------------------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutterWeb", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// --------------------------------------
// JWT AUTHENTICATION
// --------------------------------------
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

// --------------------------------------
// SWAGGER
// --------------------------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --------------------------------------
// MIDDLEWARE ORDER (ÇOK ÖNEMLİ)
// --------------------------------------
app.UseCors("AllowFlutterWeb");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// --------------------------------------
// SWAGGER UI
// --------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
