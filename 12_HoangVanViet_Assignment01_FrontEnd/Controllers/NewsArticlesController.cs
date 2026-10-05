using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using _12_HoangVanViet_Assignment01_FrontEnd.Services;
using Microsoft.AspNetCore.Http;
using _12_HoangVanViet_Assignment01_FrontEnd.Models;

namespace _12_HoangVanViet_Assignment01_FrontEnd.Controllers
{
    public class NewsArticlesController : Controller
    {
        private readonly ApiService _apiService;

        public NewsArticlesController(ApiService apiService)
        {
            _apiService = apiService;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Role") == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}
