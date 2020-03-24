using Xunit;

namespace xUnitTestSamples.Features.Tests
{
    [Collection(nameof(ClienteTestFixtureCollection))]
    public class ClienteTesteInvalido
    {
        private readonly ClienteTestsFixtureNoMock _clienteTestsFixture;

        public ClienteTesteInvalido(ClienteTestsFixtureNoMock clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Novo Cliente Inválido")]
        [Trait("Category", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.False(result);
            Assert.NotEqual(0, cliente.ValidationResult.Errors.Count);
        }
    }
}