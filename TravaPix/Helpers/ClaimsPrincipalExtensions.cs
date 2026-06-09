using System.Security.Claims;

namespace FraudDetection.Web.Helpers
{
    /// <summary>
    /// Atalhos para ler os dados do usuário logado a partir dos claims do cookie.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        public const string CpfClaim = "cpf";

        public static string GetCpf(this ClaimsPrincipal user)
            => user.FindFirstValue(CpfClaim) ?? "";

        public static string GetDisplayName(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.Name) ?? "";

        public static string GetFirstName(this ClaimsPrincipal user)
        {
            string name = user.GetDisplayName();

            return string.IsNullOrWhiteSpace(name)
                ? ""
                : name.Split(' ')[0];
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole("ADMIN");
    }
}
