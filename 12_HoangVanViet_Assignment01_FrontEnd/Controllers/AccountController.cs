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
            try
            {
                var account = await _apiService.LoginAsync(email, password);
                if (account != null)
                {
                    HttpContext.Session.SetString("User", JsonSerializer.Serialize(account));
                    HttpContext.Session.SetInt32("Role", account.AccountRole ?? 2);
                    HttpContext.Session.SetInt32("AccountId", account.AccountID);
                    HttpContext.Session.SetString("AccountName", account.AccountName ?? "User");
                    
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Error = "Email hoặc mật khẩu không chính xác.";
            }
            catch (System.Net.Http.HttpRequestException)
            {
                ViewBag.Error = "Không thể kết nối đến máy chủ BackEnd (API). Vui lòng kiểm tra xem BackEnd đã được khởi chạy chưa.";
            }
            catch (System.Exception ex)
            {
                ViewBag.Error = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
