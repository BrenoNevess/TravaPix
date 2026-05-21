using FraudDetection.Models;
using FraudDetection.Repositories;
using FraudDetection.Session;

namespace FraudDetection.Services
{
    public class AuthService
    {
        private readonly IUserRepository
            userRepository;

        public AuthService(
            IUserRepository repository
        )
        {
            userRepository = repository;
        }

        public bool Login(
            string cpf,
            string password
        )
        {
            User? user =
                userRepository.GetByCpf(cpf);

            if (
                user == null ||
                user.Password != password
            )
            {
                return false;
            }

            UserSession.Login(user);

            return true;
        }

        public void Logout()
        {
            UserSession.Logout();
        }
    }
}