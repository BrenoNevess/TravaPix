using System.ComponentModel.DataAnnotations;

namespace FraudDetection.Web.Models.ViewModels
{
    /// <summary>
    /// Formulário de nova transação. Após o POST, <see cref="Result"/> é
    /// preenchido para exibir a análise de risco na mesma página.
    /// </summary>
    public class TransactionViewModel
    {
        [Required(ErrorMessage = "Informe o CPF do remetente.")]
        [Display(Name = "CPF do remetente")]
        public string SenderCpf { get; set; } = "";

        [Required(ErrorMessage = "Informe o CPF do destinatário.")]
        [Display(Name = "CPF do destinatário")]
        public string ReceiverCpf { get; set; } = "";

        [Range(0.01, 99_999_999, ErrorMessage = "Informe um valor válido.")]
        [Display(Name = "Valor da transação")]
        public decimal Amount { get; set; }

        [Display(Name = "Localização")]
        public string Location { get; set; } = "";

        [Display(Name = "Descrição")]
        public string Description { get; set; } = "";

        public TransactionResultViewModel? Result { get; set; }
    }
}
