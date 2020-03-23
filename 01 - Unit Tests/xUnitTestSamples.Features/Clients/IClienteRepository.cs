using xUnitTestSamples.Features.Core;

namespace xUnitTestSamples.Features.Clients
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente ObterPorEmail(string email);
    }
}