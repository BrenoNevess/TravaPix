namespace FraudDetection.Web.Models;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    /// <summary>Mensagem amigável exibida ao usuário.</summary>
    public string? Message { get; set; }
}
