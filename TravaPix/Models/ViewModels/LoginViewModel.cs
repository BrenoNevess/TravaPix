using System.ComponentModel.DataAnnotations;

namespace FraudDetection.Web.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o CPF.")]
        [Display(Name = "CPF")]
        public string Cpf { get; set; } = "";

        [Required(ErrorMessage = "Informe a senha.")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; } = "";
    }
}
