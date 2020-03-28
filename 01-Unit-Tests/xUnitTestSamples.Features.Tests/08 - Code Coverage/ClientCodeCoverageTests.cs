using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Moq;
using System.Threading;
using Xunit;
using xUnitTestSamples.Features.Clients;
using Moq.AutoMock;

namespace xUnitTestSamples.Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientCodeCoverageTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClientCodeCoverageTests(ClienteTestsFixture clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Code Coverage - Testar entidade(Inativar)")]
        [Trait("Categoria", "Code Coverage - Cliente Service Tests")]
        public void Client_Inativar_DeveInativarComSucesso()
        {
            // Arrange
            Cliente cliente = _clienteTestsFixture.GenerateValidClient();

            // Act
            cliente.Inativar();

            // Assert
            Assert.False(cliente.Ativo);
        }


        [Fact(DisplayName = "Code Coverage - Testar entidade(EhEspecial)")]
        [Trait("Categoria", "Code Coverage - Cliente Service Tests")]
        public void Client_Inativar_ClienteDeveSerEspecial()
        {
            // Arrange
            Cliente cliente = _clienteTestsFixture.GenerateValidClient();

            // Act && Assert
            Assert.True(cliente.EhEspecial());
        }

        [Fact(DisplayName = "Code Coverage - Testar entidade(EhEspecial)")]
        [Trait("Categoria", "Code Coverage - Cliente Service Tests")]
        public void Client_Entity_Util()
        {
            // Arrange
            Cliente cliente1 = _clienteTestsFixture.GenerateValidClient();

            Cliente cliente2 = _clienteTestsFixture.GenerateValidClient();

            // Act && Assert
            Assert.False(cliente1.Equals(cliente2));
            Assert.True(cliente1.Equals(cliente1));
            Assert.NotNull(cliente1.GetHashCode());
            Assert.NotEmpty(cliente1.ToString());
        }
    }
}