using Authentication.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Shared.DTOs.Autenticacion;
using Shared.DTOs.Usuarios;
using Shared.Models.Entities;
using System.Threading.Tasks;

namespace Authentication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationServices _authenticationService;

        public AuthenticationController(AuthenticationServices authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = await _authenticationService.RegisterAsync(usuarioDTO);
            if (usuario.IsSuccess)
            {
                return Ok(usuario);
            }

            return BadRequest(new AuthenticationResultDTO
            {
                IsSuccess = false,
                Message = "Error registro controlador API autenticacion BACKEND",
                Token = string.Empty
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredencialesDTO authenticationUsuarioDTO)
        {
            var usuario = await _authenticationService.LoginAsync(authenticationUsuarioDTO);

            if (!usuario.IsSuccess)
            {
                return Unauthorized(new { Message = usuario.Message });
            }

            usuario.IsSuccess = true;
            usuario.Message = "Usuario autenticado con éxito BACKEND";
            
            return Ok(usuario);
        }
    }
}
