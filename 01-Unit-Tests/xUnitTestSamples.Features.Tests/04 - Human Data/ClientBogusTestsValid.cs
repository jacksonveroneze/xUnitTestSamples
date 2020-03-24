using Xunit;
using xUnitTestSamples.Features.Clients;

namespace xUnitTestSamples.Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientBogusTestsValid
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClientBogusTestsValid(ClienteTestsFixture clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Category", "Cliente Bogus Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            Cliente cliente = _clienteTestsFixture.GenerateValidClient();

            // Act
            bool result = cliente.EhValido();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}