using SMED.BackEnd.Repositories.Interface;
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

namespace SMED.BackEnd.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<UserDTO, int> _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IRepository<UserDTO, int> userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            try
            {
                var users = await _userRepository.GetAllAsync();

                var user = users.FirstOrDefault(u =>
                    u.Email != null &&
                    u.Email.Equals(loginRequest.Email, StringComparison.OrdinalIgnoreCase));

                if (user == null)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "User not found."
                    };
                }

                if (user.IsActive != true)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "User is inactive."
                    };
                }

                // En este ejemplo, aún está en texto plano.
                if (user.Password != loginRequest.Password)
                {
                    return new LoginResponseDTO
                    {
                        IsAuthenticated = false,
                        Message = "Invalid password."
                    };
                }

                var token = GenerateJwtToken(user);

                return new LoginResponseDTO
                {
                    IsAuthenticated = true,
                    Token = token,
                    Message = "Login successful.",
                    User = user
                };
            }
            catch (Exception ex)
            {
                // Puedes loggear ex.Message aquí si usas un sistema de logging
                return new LoginResponseDTO
                {
                    IsAuthenticated = false,
                    Message = "An error occurred while trying to authenticate. Please try again later."
                };
            }
        }

        private string GenerateJwtToken(UserDTO user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email ?? ""),
                    new Claim(ClaimTypes.Name, user.Name ?? "")
                }),
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
