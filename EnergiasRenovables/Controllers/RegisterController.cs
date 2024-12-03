using EnergiasRenovables.Model.DTO;
using EnergiasRenovables.Services;
using Microsoft.AspNetCore.Mvc;

namespace EnergiasRenovables.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegisterController(AuthService authService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] UsuarioRegister request)
    {
        var success = await authService.RegisterAsync(request.UserName, request.Password, request.Email);
        if (!success)
            return BadRequest(new { Message = "El usuario ya existe." });

        return Ok(new { Message = "Usuario registrado exitosamente." });
    }
}