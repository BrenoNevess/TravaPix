namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Motor de análise de risco de transações (valor, horário e localização).
    /// </summary>
    public interface IFraudDetectionService
    {
        FraudAnalysisResult Analyze(
            decimal amount,
            DateTime when,
            string? transactionLocation,
            string? userLocation);
    }
}
