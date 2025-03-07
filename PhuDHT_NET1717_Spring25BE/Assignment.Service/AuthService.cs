using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.Data;
using Assignment.Data.Models;
using Assignment.Data.Repository;
using Assignment.Service.Dto;
using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.Service
{
    public interface IAuthService
    {
        string GenerateJwtToken(User user);
        Task<string?> AuthenticateUserAsync(string username, string password);
        Task<string> RegisterAsync(RegisterDto registerDto);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly UnitOfWork _unitOfWork;

        public AuthService(UnitOfWork unitOfWork, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _config = config;
        }

        public async Task<string?> AuthenticateUserAsync(string username, string password)
        {
            var user = await _unitOfWork.UserRepository.GetUserByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.HashedPassword))
            {
                return null; // Trả về null nếu xác thực thất bại
            }

            return GenerateJwtToken(user);
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("JwtSettings");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role) // Thêm role vào token
        };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            // Kiểm tra username đã tồn tại chưa
            var existingUser = await _unitOfWork.UserRepository.GetUserByUsernameAsync(registerDto.Username);
            if (existingUser != null)
            {
                return "Username đã tồn tại.";
            }

            // Hash password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            // Tạo user mới
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Username,
                HashedPassword = hashedPassword,
                Role = registerDto.Role
            };

            await _unitOfWork.UserRepository.CreateAsync(newUser);

            return "Đăng ký thành công!";
        }
    }
}
