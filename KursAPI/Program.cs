using KursAPI.Models;
using KursAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<Kursdb15Context>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<ChitateliService, ChitateliService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options=>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            // указывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer=true,
            // строка, представляющая издателя
            ValidIssuer=AuthOptions.ISSUER,
            // будет ли валидироваться потребитель токена
            ValidateAudience=true,
            // установка потребителя токена
            ValidAudience=AuthOptions.ISSUER,
            // будет ли валидироваться время существования
            ValidateLifetime=true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthentication();
app.MapPost("/login", (Person loginData,Kursdb15Context db) => 
{
    Person person=db.Persons.FirstOrDefault(p=>p.Email== loginData.Email&&p.Password==loginData.Password)!;
    if (person is null) return Results.Unauthorized();
    
    var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.Email) };
    var jwt = new JwtSecurityToken(issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
    var encodedJWT= new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encodedJWT,
        username = person.Email
    };
    return Results.Json(response);
});

app.UseAuthorization();
app.MapControllers();
app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer"; // издатель токена
    public const string AUDIENCE = "MyAuthClient"; // потребитель токена
    const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
    public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
      new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
}
