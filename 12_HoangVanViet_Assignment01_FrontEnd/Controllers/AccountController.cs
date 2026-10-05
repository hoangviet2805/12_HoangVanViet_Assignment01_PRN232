using _12_HoangVanViet_Assignment01_FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace _12_HoangVanViet_Assignment01_FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApiService _apiService;

        public AccountController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var account = await _apiService.LoginAsync(email, password);
            if (account != null)
            {
                HttpContext.Session.SetString("User", JsonSerializer.Serialize(account));
                HttpContext.Session.SetInt32("Role", account.AccountRole ?? 2);
                
                if (account.AccountRole == 0) // Admin
                {
                    return RedirectToAction("Index", "SystemAccounts");
                }
                else // Staff
                {
                    return RedirectToAction("Index", "NewsArticles");
                }
            }

            ViewBag.Error = "Invalid login attempt.";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
