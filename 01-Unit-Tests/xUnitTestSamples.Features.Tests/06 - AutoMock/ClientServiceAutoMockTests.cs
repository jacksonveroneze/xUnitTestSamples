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
    public class ClientServiceAutoMockTests
    {
        private readonly ClienteTestsFixture _clienteTestsFixture;

        public ClientServiceAutoMockTests(ClienteTestsFixture clienteTestsFixture)
            => _clienteTestsFixture = clienteTestsFixture;

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClientService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            Cliente cliente = _clienteTestsFixture.GenerateValidClient();

            AutoMocker mocker = new AutoMocker();

            IClienteService clienteService = mocker.CreateInstance<ClienteService>();
            Mock<IClienteRepository> clienteRepository = mocker.GetMock<IClienteRepository>();
            Mock<IMediator> mediatr = mocker.GetMock<IMediator>();

            // Act
            clienteService.Adicionar(cliente);

            // Assert
            Assert.True(cliente.EhValido());
            clienteRepository.Verify(r => r.Adicionar(cliente), Times.Once);
            mediatr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None));
        }

        [Fact(DisplayName = "Obter Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Tests")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange
            AutoMocker mocker = new AutoMocker();

            IClienteService clienteService = mocker.CreateInstance<ClienteService>();

            Mock<IClienteRepository> clienteRepository = mocker.GetMock<IClienteRepository>();

            clienteRepository.Setup(c => c.ObterTodos())
                .Returns(_clienteTestsFixture.GenerateColletionOfValidClients());

            // Act
            IEnumerable<Cliente> clients = clienteService.ObterTodosAtivos();

            // Assert
            clienteRepository.Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clients.Any());
            Assert.True(clients.Count(x => !x.Ativo) == 0);
        }
    }
}
