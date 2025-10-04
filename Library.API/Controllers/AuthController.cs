using Library.Application.Features.Auth.LoginUser;
using Library.Application.Features.Auth.RegisterUser;
using Library.Application.Features.Auth.UseRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator _mediatR) : ControllerBase
    {
        public record RegisterRequest(string UserName, string Email, string Password, bool AsAdmin = false);
        public record LoginRequest(string UserName, string Password);
        public record RefreshTokenRequest(Guid UserId, string RefreshToken);

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _mediatR.Send(new RegisterUserCommand(request.UserName, request.Email, request.Password, request.AsAdmin));
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _mediatR.Send(new LoginUserCommand(request.UserName, request.Password));
            return Ok(result);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var result = await _mediatR.Send(new RefreshTokenCommand(request.UserId, request.RefreshToken));
            return Ok(result);
        }
    }
}
