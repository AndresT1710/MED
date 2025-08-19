﻿using SMED.BackEnd.Repositories.Interface;
using SMED.Shared.DTOs;
using SMED.BackEnd.Services.Interface;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SGIS.Models;
using SMED.Shared.Entity;

namespace SMED.BackEnd.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserDTO, int> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly SGISContext _context;

        public AuthService(
            IRepository<UserDTO, int> userRepository,
            IConfiguration configuration,
            SGISContext context)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _context = context;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                // Buscar usuario por email
                var user = await _context.Users
                    .Include(u => u.PersonNavigation)
                        .ThenInclude(p => p.HealthProfessional)
                            .ThenInclude(hp => hp.HealthProfessionalTypeNavigation)
                    .FirstOrDefaultAsync(u => u.PersonNavigation.Email == loginRequest.Email);

                if (user == null)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "Usuario no encontrado."
                    };
                }

                if (user.IsActive != true)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "Usuario inactivo."
                    };
                }

                // Verificar contraseña (en producción usar hash)
                if (user.Password != loginRequest.Password)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "Contraseña incorrecta."
                    };
                }

                // Determinar si es admin (no tiene registro en HealthProfessional)
                bool isAdmin = user.PersonNavigation?.HealthProfessional == null;

                // Crear UserDTO con información completa
                var userDto = new UserDTO
                {
                    Id = user.Id,
                    PersonId = user.PersonId,
                    Email = user.PersonNavigation?.Email,
                    Name = user.PersonNavigation != null
                        ? string.Join(" ", new[] {
                            user.PersonNavigation.FirstName,
                            user.PersonNavigation.LastName,
                        }.Where(s => !string.IsNullOrWhiteSpace(s)))
                        : "Usuario",
                    IsActive = user.IsActive,
                    Password = null
                };

                Console.WriteLine($"[Backend AuthService] UserDto Name before token generation: '{userDto.Name}'");

                var token = GenerateJwtToken(userDto, isAdmin, user.PersonNavigation?.HealthProfessional);

                return new LoginResponseDTO
                {
                    IsAuthenticated = true,
                    Token = token,
                    Message = "Login exitoso.",
                    User = userDto
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Backend AuthService] Error during authentication: {ex.Message}");
                return new LoginResponseDTO
                {
                    IsAuthenticated = false,
                    Message = "Error durante la autenticación. Intente nuevamente."
                };
            }
        }

        private string GenerateJwtToken(UserDTO user, bool isAdmin, HealthProfessional? healthProfessional)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.PersonId.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(ClaimTypes.Name, user.Name ?? ""),
                new Claim("PersonId", user.PersonId?.ToString() ?? ""),
                new Claim("IsAdmin", isAdmin.ToString())
            };

            if (healthProfessional != null)
            {
                claims.Add(new Claim("HealthProfessionalTypeId", healthProfessional.HealthProfessionalTypeId?.ToString() ?? ""));
                claims.Add(new Claim("ProfessionalTypeName", healthProfessional.HealthProfessionalTypeNavigation?.Name ?? ""));
                claims.Add(new Claim("RegistrationNumber", healthProfessional.RegistrationNumber ?? ""));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpirationMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
