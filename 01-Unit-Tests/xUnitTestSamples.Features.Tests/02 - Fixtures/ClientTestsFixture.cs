using System;
using Xunit;
using xUnitTestSamples.Features.Clients;

namespace xUnitTestSamples.Features.Tests
{
    [CollectionDefinition(nameof(ClienteTestFixtureCollection))]
    public class ClienteTestFixtureCollection : ICollectionFixture<ClienteTestsFixtureNoMock> { }

    public class ClienteTestsFixtureNoMock : IDisposable
    {
        public Cliente GerarClienteValido()
            => new Cliente(Guid.NewGuid(), "Eduardo", "Pires", DateTime.Now.AddYears(-30), "edu@edu.com", true, DateTime.Now);

        public Cliente GerarClienteInValido()
            => new Cliente(Guid.NewGuid(), "", "", DateTime.Now, "edu2edu.com", true, DateTime.Now);

        public void Dispose() { }
    }
}