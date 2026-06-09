using FraudDetection.Web.Services;

namespace TravaPix.Tests
{
    public class PasswordValidatorTests
    {
        [Theory]
        [InlineData("Senha@2026", true)]
        [InlineData("curta1!", false)]
        [InlineData("semmaiuscula1!", false)]
        [InlineData("SemNumero!", false)]
        [InlineData("SemEspecial1", false)]
        public void Validate_ReturnsExpected(string password, bool expected)
        {
            bool valid = PasswordValidator.Validate(
                password,
                name: "Joao Teste",
                cpf: "11122233344",
                email: "joao@teste.com",
                out _);

            Assert.Equal(expected, valid);
        }
    }
}
