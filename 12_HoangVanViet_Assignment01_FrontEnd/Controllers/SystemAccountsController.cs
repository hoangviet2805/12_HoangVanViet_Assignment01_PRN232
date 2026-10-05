using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _12_HoangVanViet_Assignment01_FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using _12_HoangVanViet_Assignment01_FrontEnd.Models;

namespace _12_HoangVanViet_Assignment01_FrontEnd.Controllers
{
    public class SystemAccountsController : Controller
    {
        private readonly ApiService _apiService;

        public SystemAccountsController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetInt32("Role") != 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var accounts = await _apiService.GetSystemAccountsAsync();
            return View(accounts);
        }
    }
}
