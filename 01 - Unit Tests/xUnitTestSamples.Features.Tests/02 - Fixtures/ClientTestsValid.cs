using Xunit;

namespace xUnitTestSamples.Features.Tests
{
    [Collection(nameof(ClienteTestFixtureCollection))]
    public class ClienteTesteValido
    {
        private readonly ClienteTestsFixtureNoMock _clienteTestsFixture;

        public ClienteTesteValido(ClienteTestsFixtureNoMock clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Novo Cliente Válido")]
        [Trait("Category", "Cliente Fixture Testes")]
        public void Cliente_NovoCliente_DeveEstarValido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert 
            Assert.True(result);
            Assert.Equal(0, cliente.ValidationResult.Errors.Count);
        }
    }
}