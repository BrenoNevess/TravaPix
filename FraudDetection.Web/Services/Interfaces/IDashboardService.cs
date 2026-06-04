using FraudDetection.Web.Models.ViewModels;

namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Monta os indicadores da visão geral do usuário.
    /// </summary>
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync(string cpf, string firstName);
    }
}
