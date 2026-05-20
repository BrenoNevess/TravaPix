using FraudDetection.Models;

namespace FraudDetection.Session
{
    public static class UserSession
    {
        public static User? CurrentUser { get; private set; }

        public static bool IsLogged =>
            CurrentUser != null;

        public static bool IsAdmin =>
            CurrentUser?.IsAdmin == true;

        public static void Login(User user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}