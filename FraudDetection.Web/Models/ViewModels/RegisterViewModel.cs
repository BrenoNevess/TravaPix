using System.ComponentModel.DataAnnotations;

namespace FraudDetection.Web.Models.ViewModels
{
    public class RegisterViewModel
    {
        //Dados do usuário

        [Required(ErrorMessage = "Informe o nome completo.")]
        [Display(Name = "Nome completo")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Informe o CPF.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = "";

        [Required(ErrorMessage = "Informe o e-mail.")]
        [EmailAddress(ErrorMessage = "O e-mail precisa ser válido.")]
        [Display(Name = "E-mail")]
        public string Email { get; set; } = "";

        [Display(Name = "Localização (Cidade/Estado)")]
        public string? Location { get; set; }

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Confirme a senha.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "As senhas precisam ser iguais.")]
        [Display(Name = "Confirmar senha")]
        public string ConfirmPassword { get; set; } = "";

        // ----- Dados do cartão -----

        [Required(ErrorMessage = "Informe o número do cartão.")]
        [Display(Name = "Número do cartão")]
        public string CardNumber { get; set; } = "";

        [Required(ErrorMessage = "Informe o CVV.")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV deve ter 3 ou 4 dígitos.")]
        [Display(Name = "CVV")]
        public string CardCvv { get; set; } = "";

        [Required(ErrorMessage = "Informe a validade (MM/AA).")]
        [Display(Name = "Validade (MM/AA)")]
        public string ExpiryDate { get; set; } = "";

        [Range(0.01, 9_999_999, ErrorMessage = "Informe um limite válido.")]
        [Display(Name = "Limite do cartão")]
        public decimal CreditLimit { get; set; }
    }
}
