using FraudDetection.Web.Exceptions;

namespace FraudDetection.Web.Middleware
{
    /// <summary>
    /// Rede de segurança global: registra qualquer exceção não tratada e
    /// redireciona para a página de erro com uma mensagem amigável.
    /// Erros de regra de negócio (<see cref="DomainException"/>) normalmente são
    /// tratados nos controllers; aqui ficam apenas como fallback.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(ex, "Regra de negócio violada.");
                Redirect(context, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro não tratado.");
                Redirect(context, "Ocorreu um erro inesperado. Tente novamente.");
            }
        }

        private static void Redirect(HttpContext context, string message)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            context.Response.Redirect($"/Home/Error?message={Uri.EscapeDataString(message)}");
        }
    }
}
