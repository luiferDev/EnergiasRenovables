using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EnergiasRenovables.Model.Entities;
using Microsoft.IdentityModel.Tokens;

namespace EnergiasRenovables.Services;

public class AuthService(UserRepository userRepository, IConfiguration configuration)
{
    public async Task<bool> RegisterAsync(string username, string password, string? email)
    {
        if (await userRepository.GetByUsernameAsync(username) != null)
            return false; // Usuario ya existe.

        var hashedPassword = PasswordHasher.HashPassword(password);

        var user = new Usuario
        {
            UserName = username,
            Password = hashedPassword,
            Role = "User",
            Email = email
        };

        await userRepository.AddUserAsync(user);
        return true;
    }

    public async Task<string> LoginAsync(string username, string password)
    {
        var user = await userRepository.GetByUsernameAsync(username);
        if (user == null || !PasswordHasher.VerifyPassword(password, user.Password))
            return "Credenciales inválidas"; // Credenciales inválidas.

        // Aquí puedes generar un token JWT (ver paso 6).
        return GenerateJwtToken(user);
    }

    public string GenerateJwtToken(Usuario user)
    {
        var jwtSettings = configuration.GetSection("Jwt");

        var tokenHandler = new JwtSecurityTokenHandler();
        var byteKey = Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException());
        var tokenDes = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName ?? throw new InvalidOperationException()), 
                new Claim(ClaimTypes.Role, user.Role),
            }), 
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(byteKey), 
                SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDes);
        return tokenHandler.WriteToken(token);
    }
}
