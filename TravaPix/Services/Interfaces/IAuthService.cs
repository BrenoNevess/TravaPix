using FraudDetection.Web.Models.Entities;
using FraudDetection.Web.Models.ViewModels;

namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Cadastro e autenticação de usuários.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Registra um novo usuário (com cartão). Lança <see cref="Exceptions.DomainException"/>
        /// quando alguma regra de negócio é violada.
        /// </summary>
        Task<User> RegisterAsync(RegisterViewModel model);

        /// <summary>
        /// Retorna o usuário se o CPF existir e a senha conferir; caso contrário,
        /// lança <see cref="Exceptions.DomainException"/>.
        /// </summary>
        Task<User> ValidateCredentialsAsync(string cpf, string password);
    }
}
