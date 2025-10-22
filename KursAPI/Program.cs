using KursAPI.Models;
using KursAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            ValidAudience=AuthOptions.AUDIENCE,
            // будет ли валидироваться время существования
            ValidateLifetime=true,
            // установка ключа безопасности
            IssuerSigningKey = AuthOptions.GetSimmetricSecurutyKey(),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = true
        };
    });

var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapPost("/login", async (Person user, Kursdb15Context db) =>
{
    Person? person = await db.Persons!.FirstOrDefaultAsync(p => p.Email == user.Email && p.Password == user.Password);
    if (person is null) return Results.Unauthorized();
    var claims = new List<Claim> { new Claim(ClaimTypes.Email, user.Email) };
    var jwt = new JwtSecurityToken
    (
        issuer: AuthOptions.ISSUER,
        audience: AuthOptions.AUDIENCE,
        claims: claims,
        expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(5)),
        signingCredentials: new SigningCredentials(AuthOptions.GetSimmetricSecurutyKey(), SecurityAlgorithms.HmacSha256));
    var encoderJWT = new JwtSecurityTokenHandler().WriteToken(jwt);
    var response = new
    {
        access_token = encoderJWT,
        username = person.Email
    };
    return Results.Json(response);
}
);
app.MapPost("/register", async (Person user, Kursdb15Context db) =>
{
    byte[] salt = AuthOptions.GenerateSalt();
    byte[] sha256Hash = AuthOptions.GenerateSha256Hash(user.Password, salt);
    user.Password= Convert.ToBase64String(sha256Hash);
    db.Persons.Add(user);
    await db.SaveChangesAsync();
});
app.Run();

public class AuthOptions
{
    public const string ISSUER = "MyAuthServer";
    public const string AUDIENCE = "MyAuthClient";
    const string KEY = "mysupersecret_secretsecretkey!123";
    public static SymmetricSecurityKey GetSimmetricSecurutyKey() =>
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    public static byte[] GenerateSalt()
    {
        const int SaltLength = 64;
        byte[] salt = new byte[SaltLength];

        var rngRand = new RNGCryptoServiceProvider();
        rngRand.GetBytes(salt);

        return salt;
    }
    public static byte[] GenerateSha256Hash(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

        using var hash = new SHA256CryptoServiceProvider();

        return hash.ComputeHash(saltedPassword);
    }
}

