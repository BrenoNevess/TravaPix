namespace FraudDetection.Web.Models.Enums
{
    /// <summary>
    /// Ação que o sistema deve tomar para uma transação, definida pelo motor de
    /// detecção (não é persistida — usada apenas durante o fluxo).
    /// </summary>
    public enum FraudDecision
    {
        /// <summary>Transação liberada (apenas confirma os dados).</summary>
        Allow,

        /// <summary>Suspeita: exige assinatura e ciência do risco antes de completar.</summary>
        RequireConfirmation,

        /// <summary>Bloqueada: não é concluída e o destinatário é bloqueado por 8h.</summary>
        Block
    }
}
