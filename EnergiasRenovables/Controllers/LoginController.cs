using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController(AuthService authService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> LoginUsuario([FromBody] UsuarioLoginDto login)
    {
        var token = await authService.LoginAsync(login.UserName, login.Password);
        if (token == null)
            return Unauthorized(new { Message = "Credenciales inválidas." });

        return Ok(new { Token = token });
    }
}