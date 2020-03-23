using Xunit;

namespace xUnitTestSamples.Features.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    public class OrdemTestes
    {
        public static bool Teste1Chamado;
        public static bool Teste2Chamado;


        [Fact(DisplayName = "Teste 01"), TestPriority(2)]
        [Trait("Category", "Ordenacao Testes")]
        public void Teste01()
        {
            Teste1Chamado = true;

            Assert.False(Teste2Chamado);
        }

        [Fact(DisplayName = "Teste 02"), TestPriority(4)]
        [Trait("Category", "Ordenacao Testes")]
        public void Teste02()
        {
            Teste2Chamado = true;

            Assert.True(Teste1Chamado);
        }
    }
}