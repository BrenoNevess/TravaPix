namespace FraudDetection.Web.Exceptions
{
    /// <summary>
    /// Erro de regra de negócio com mensagem amigável ao usuário
    /// (ex.: "CPF já cadastrado", "Senha inválida").
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
