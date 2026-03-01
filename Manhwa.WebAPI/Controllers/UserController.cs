using Manhwa.Application.Features.User.ChangePassword;
using Manhwa.Application.Features.User.GetCurrentUser;
using Manhwa.Application.Features.User.Login;
using Manhwa.Application.Features.User.Register;
using Manhwa.Application.Features.User.Update;
using Manhwa.Application.Features.User.VerifyPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Manhwa.WebAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserRequest request)
        {
            var command = new RegisterUserCommand
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginUserCommand
            {
                Identifier = request.Identifier,
                Password = request.Password
            };

            var result = await _mediator.Send(command);

            Response.Cookies.Append("accessToken", result.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,            
                SameSite = SameSiteMode.None,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            });

            return Ok(new
            {
                result.Id,
                result.Username,
                result.Email
            });
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("accessToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });

            return Ok(new { message = "Logged out successfully" });
        }
        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile(UpdateUserRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new UpdateUserCommand
            {
                UserId = userId,
                Username = request.Username,
                Email = request.Email
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new ChangePasswordCommand
            {
                UserId = userId,
                CurrentPassword = request.CurrentPassword,
                NewPassword = request.NewPassword
            };

            await _mediator.Send(command);
            return Ok(new { message = "Password changed successfully" });
        }
        [Authorize]
        [HttpPost("verify-password")]
        public async Task<IActionResult> VerifyPassword(VerifyPasswordRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var command = new VerifyPasswordCommand
            {
                UserId = userId,
                Password = request.Password
            };

            var isValid = await _mediator.Send(command);

            return Ok(new { valid = isValid });
        }
        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _mediator.Send(new GetCurrentUserQuery
            {
                UserId = userId
            });

            return Ok(result);
        }
    }
}
