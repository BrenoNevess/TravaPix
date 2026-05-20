using FraudDetection.Models;
using FraudDetection.Repositories;
using FraudDetection.Session;

namespace FraudDetection.Services
{
    public class AuthService
    {
        private readonly IUserRepository
            _userRepository;

        public AuthService()
        {
            _userRepository =
                new FakeUserRepository();
        }

        public bool Register(User user)
        {
            User? existingCpf =
                _userRepository.GetByCpf(
                    user.Cpf
                );

            if (existingCpf != null)
            {
                return false;
            }

            _userRepository.Add(user);

            return true;
        }

        public bool Login(
            string cpf,
            string password
        )
        {
            User? user =
                _userRepository.GetByCpf(
                    cpf
                );

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