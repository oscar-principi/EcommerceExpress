using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using Shared.DTOs;
using Shared.DTOs.Usuarios;
using Shared.DTOs.Autenticacion;
using Shared.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.API.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Azure;
using System.Data;

namespace Authentication.API.Services
{
    public class AuthenticationServices
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyIdentityDBContext _context;

        public AuthenticationServices(UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, MyIdentityDBContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<AuthenticationResultDTO> RegisterAsync(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                UserName = usuarioDTO.Email,
                FirstName = usuarioDTO.FirstName,
                LastName = usuarioDTO.LastName,
                Dni = usuarioDTO.Dni,
                Email = usuarioDTO.Email,
                Registered = DateTime.Now
            };

            var result = await _userManager.CreateAsync(usuario, usuarioDTO.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(usuario, "usuario");

                var token = GenerateJwtToken(usuario);

                if (string.IsNullOrEmpty(token))
                {
                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        Message = "Error al generar el token",
                        Token = string.Empty
                    };
                }

                return new AuthenticationResultDTO
                {
                    IsSuccess = true,
                    Message = "Usuario registrado con éxito",
                    Token = token
                };
            }

            return new AuthenticationResultDTO
            {
                IsSuccess = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description)),
                Token = string.Empty
            };
        }



        public async Task<AuthenticationResultDTO> LoginAsync(CredencialesDTO usuarioLoginDTO)
        {
            var user = await _userManager.FindByEmailAsync(usuarioLoginDTO.Email);

            if (user == null)
            {
                return new AuthenticationResultDTO
                {
                    IsSuccess = false,
                    Message = "No se encontró el usuario BACKEND"
                };
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, usuarioLoginDTO.Password, true, true);

            if (signInResult.Succeeded)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == user.Id);  

                if (usuario == null)
                {
                    return new AuthenticationResultDTO
                    {
                        IsSuccess = false,
                        Message = "No se encontró información completa del usuario BACKEND"
                    };
                }

                var token = GenerateJwtToken(usuario);

                return new AuthenticationResultDTO
                {
                    IsSuccess = true,
                    Message = "Usuario autenticado con éxito BACKEND",
                    Token = token
                };
            }
            else
            {
                Console.WriteLine("El inicio de sesión falló.");
            }

            return new AuthenticationResultDTO
            {
                IsSuccess = false,
                Message = "Credenciales inválidas BACKEND"
            };
        }


        private string GenerateJwtToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.UserName)
            };

            var roles = _userManager.GetRolesAsync(usuario).Result;

            if (roles.Any())
            {
                claims.Add(new Claim(ClaimTypes.Role, roles.First()));
            }

            var secretKey = _configuration["JwtSettings:SecretKey"];
            var issuer = _configuration["JwtSettings:Issuer"];
            var audience = _configuration["JwtSettings:Audience"];

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
