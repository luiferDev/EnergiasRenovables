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
            return null; // Credenciales inválidas.

        // Aquí puedes generar un token JWT (ver paso 6).
        return GenerateJwtToken(user);
    }

    public string GenerateJwtToken(Usuario user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, ""),//request.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, "User") // Agregar roles si es necesario.
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] 
                                                                  ?? throw new InvalidOperationException()));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds);
        
        //return token = new JwtSecurityTokenHandler().WriteToken(token)
        return "hola";
    }
}
