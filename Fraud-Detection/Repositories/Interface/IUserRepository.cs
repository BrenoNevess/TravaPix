using System.Collections.Generic;

using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);

        List<User> GetAll();

        User? GetByCpf(string cpf);

        void Remove(User user);
    }
}