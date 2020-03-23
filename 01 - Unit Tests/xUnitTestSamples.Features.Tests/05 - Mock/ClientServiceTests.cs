using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using Moq;
using System.Threading;
using Xunit;
using xUnitTestSamples.Features.Clients;

namespace xUnitTestSamples.Features.Tests
{
    [Collection(nameof(ClientCollection))]
    public class ClientServiceTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClientServiceTests(ClienteTestsFixture clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Category", "Cliente Service Mock Tests")]
        public void ClientService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            Cliente cliente = _clienteTestsFixture.GenerateValidClient();

            Mock<IClienteRepository> clienteRepository = new Mock<IClienteRepository>();
            Mock<IMediator> mediatr = new Mock<IMediator>();

            IClienteService clienteService = new ClienteService(clienteRepository.Object, mediatr.Object);

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None));
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service Mock Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            Mock<IClienteRepository> clienteRepository = new Mock<IClienteRepository>();
            Mock<IMediator> mediatr = new Mock<IMediator>();

            clienteRepository.Setup(c => c.ObterTodos())
                .Returns(_clienteTestsFixture.GenerateColletionOfValidClients());

            IClienteService clienteService = new ClienteService(clienteRepository.Object, mediatr.Object);

            // Act
            IEnumerable<Cliente> clients = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clients.Any());
            Assert.True(clients.Count(x => !x.Ativo) == 0);
        }
    }
}
