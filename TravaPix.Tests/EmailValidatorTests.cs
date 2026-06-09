using FraudDetection.Web.Services;

namespace TravaPix.Tests
{
    public class EmailValidatorTests
    {
        [Theory]
        [InlineData("joao@teste.com", true)]
        [InlineData("a@b.co", true)]
        [InlineData("invalido", false)]
        [InlineData("sem@dominio", false)]
        [InlineData("", false)]
        public void Validate_ReturnsExpected(string email, bool expected)
        {
            Assert.Equal(expected, EmailValidator.Validate(email));
        }
    }
}
