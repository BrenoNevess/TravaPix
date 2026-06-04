namespace FraudDetection.Web.Services.Interfaces
{
    /// <summary>
    /// Motor de análise de risco de transações.
    /// </summary>
    public interface IFraudDetectionService
    {
        FraudAnalysisResult Analyze(decimal amount, DateTime when);
    }
}
