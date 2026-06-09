using FraudDetection.Web.Models.ViewModels;

namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Monta os indicadores da visão geral do usuário. Para administradores,
    /// inclui também o saldo total do sistema.
    /// </summary>
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDashboardAsync(string cpf, string firstName, bool isAdmin);
    }
}
