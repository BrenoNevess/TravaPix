using System.Collections.Generic;
using System.Linq;
using FraudDetection.Models;

namespace FraudDetection.Repositories
{
    public class FakeUserRepository
        : IUserRepository
    {
        private static readonly List<User>
            users = new();

        public void Add(User user)
        {
            users.Add(user);
        }

        public User? GetByCpf(string cpf)
        {
            return users.FirstOrDefault(
                x => x.Cpf == cpf
            );
        }

        public List<User> GetAll()
        {
            return users;
        }
    }
}