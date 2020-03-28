using Bogus;
using Bogus.DataSets;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using xUnitTestSamples.Features.Clients;

namespace xUnitTestSamples.Features.Tests
{
    [CollectionDefinition(nameof(ClientCollection))]
    public class ClientCollection : ICollectionFixture<ClienteTestsFixture> { }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GenerateValidClient()
            => GenerateValidClients(1, true).FirstOrDefault();

        public IList<Cliente> GenerateColletionOfValidClients()
        {
            List<Cliente> listClientes = new List<Cliente>();

            listClientes.AddRange(GenerateValidClients(50, true).ToList());
            listClientes.AddRange(GenerateValidClients(50, false).ToList());

            return listClientes;
        }

        public IEnumerable<Cliente> GenerateValidClients(int quantity, bool active)
        {
            Name.Gender gender = new Faker().PickRandom<Name.Gender>();

            Faker<Cliente> clients = new Faker<Cliente>("pt_BR")
                .CustomInstantiator(f => new Cliente(
                    Guid.NewGuid(),
                    f.Name.FirstName(gender),
                    f.Name.LastName(gender),
                    f.Date.Past(80, DateTime.Now.AddYears(-18)),
                    string.Empty,
                    active,
                    DateTime.Now.AddYears(-5)
                ))
                .RuleFor(c => c.Email, (f, c) => f.Internet.Email(c.Nome.ToLower(), c.Sobrenome.ToLower()));

            return clients.Generate(quantity);
        }

        public void Dispose() { }
    }
}