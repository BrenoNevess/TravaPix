using FraudDetection.Web.Helpers;
using FraudDetection.Web.Models.ViewModels;
using FraudDetection.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetection.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            DashboardViewModel model = await _dashboardService.GetDashboardAsync(
                User.GetCpf(),
                User.GetFirstName(),
                User.IsAdmin());

            return View(model);
        }
    }
}
