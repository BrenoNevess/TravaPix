namespace FraudDetection.Web.Models.Enums
{
    /// <summary>
    /// Nível de risco atribuído a uma transação pelo motor de detecção.
    /// </summary>
    public enum FraudRiskLevel
    {
        Safe,
        Suspicious,
        HighRisk
    }
}
