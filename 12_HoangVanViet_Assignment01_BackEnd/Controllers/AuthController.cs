using _12_HoangVanViet_Assignment01_BackEnd.Models;
using _12_HoangVanViet_Assignment01_BackEnd.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace _12_HoangVanViet_Assignment01_BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ISystemAccountRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthController(ISystemAccountRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var adminEmail = _configuration["AdminAccount:Email"];
            var adminPassword = _configuration["AdminAccount:Password"];

            if (request.Email == adminEmail && request.Password == adminPassword)
            {
                return Ok(new SystemAccount
                {
                    AccountID = 0,
                    AccountEmail = adminEmail,
                    AccountRole = 0 // Admin role
                });
            }

            var account = _repository.GetAccountByEmail(request.Email);
            if (account != null && account.AccountPassword == request.Password)
            {
                return Ok(account);
            }

            return Unauthorized("Invalid email or password.");
        }
    }

    public class LoginRequest
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
