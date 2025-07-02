using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolWebApplication.DTOs;
using SchoolWebApplication.Entities;
using SchoolWebApplication.Services;
using Microsoft.AspNetCore.Authorization;

namespace SchoolWebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtService _jwtService;

        public AuthController(UserManager<User> userManager, JwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var user = new User
            {
                Email = dto.Email,
                UserName = dto.UserName,
                DisplayName = dto.UserName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            await _userManager.AddToRoleAsync(user, dto.Role);
            return Ok("Реєстрація успішна. Користувач доданий до ролі: " + dto.Role);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                    return Unauthorized("Невірна пошта або пароль");

                var roles = await _userManager.GetRolesAsync(user);
                var accessToken = _jwtService.GenerateToken(user, roles);
                var refreshToken = _jwtService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                await _userManager.UpdateAsync(user);

                return Ok(new TokenResponseDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Помилка: " + ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshDto dto)
        {
            var principal = _jwtService.GetPrincipalFromExpiredToken(dto.AccessToken);
            if (principal == null) return BadRequest("Недійсний токен");

            var username = principal.Identity?.Name;
            if (username == null) return BadRequest("Помилка у токені");

            var user = await _userManager.FindByNameAsync(username);
            if (user == null ||
                user.RefreshToken != dto.RefreshToken ||
                user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return Unauthorized("Недійсний або прострочений Refresh токен");
            }

            var roles = await _userManager.GetRolesAsync(user);
            var newAccessToken = _jwtService.GenerateToken(user, roles);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userManager.UpdateAsync(user);

            return Ok(new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }
    }
}
