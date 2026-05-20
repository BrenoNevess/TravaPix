using System.Collections.Generic;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        User? GetByCpf(string cpf);

        List<User> GetAll();
    }
}