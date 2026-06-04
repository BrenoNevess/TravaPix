using System.Diagnostics;
using FraudDetection.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetection.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Página inicial pública. Usuário logado vai direto para o dashboard.
        /// </summary>
        public IActionResult Index()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? message)
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                Message = message
            });
        }
    }
}
