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

        public void Add(
            User user
        )
        {
            users.Add(user);
        }

        public List<User> GetAll()
        {
            return users;
        }

        public User? GetByCpf(
            string cpf
        )
        {
            return users.FirstOrDefault(
                u => u.Cpf == cpf
            );
        }

        public void Remove(
            User user
        )
        {
            users.Remove(user);
        }
    }
}