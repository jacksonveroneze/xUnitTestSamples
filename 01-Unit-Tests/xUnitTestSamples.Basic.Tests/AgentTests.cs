using System;
using Xunit;

namespace xUnitTestSamples.Basic.Tests
{
    public class AgentTests
    {
        [Theory(DisplayName = "FaixasSalariaisDevemRespeitarNivelProfissional")]
        [Trait("Category", "Agent Test")]
        [InlineData(700)]
        [InlineData(1500)]
        [InlineData(2000)]
        [InlineData(7500)]
        [InlineData(8000)]
        [InlineData(15000)]
        public void Agent_Salario_FaixasSalariaisDevemRespeitarNivelProfissional(double salario)
        {
            // Arrange && Act
            Agent agent = new Agent("Jackson", salario);

            // Assert
            if (agent.ProfessionalLevel == ProfessionalLevel.Junior)
                Assert.InRange(agent.Salary, 500, 1999);

            if (agent.ProfessionalLevel == ProfessionalLevel.Pleno)
                Assert.InRange(agent.Salary, 2000, 7999);

            if (agent.ProfessionalLevel == ProfessionalLevel.Senior)
                Assert.InRange(agent.Salary, 8000, double.MaxValue);
        }

        [Fact(DisplayName = "DeveRetornarInstanciaDeFuncioario")]
        [Trait("Category", "Agent Test")]
        public void AgentFactory_Criar_DeveRetornarInstanciaDeFuncioario()
        {
            // Arrange && Act
            Agent agent = AgentFactory.Create("Jackson", 1500);

            // Assert
            Assert.IsType<Agent>(agent);
        }

        [Fact(DisplayName = "ClasseFuncinarioDeveSerUmaSubClassDePessoa")]
        [Trait("Category", "Agent Test")]
        public void AgentFactory_Criar_ClasseFuncinarioDeveSerUmaSubClassDePessoa()
        {
            // Arrange && Act
            Agent agent = AgentFactory.Create("Jackson", 1500);

            // Assert
            Assert.IsAssignableFrom<Person>(agent);
        }

        [Fact(DisplayName = "CriarDeveRetornarErroSalarioInferiorAoPermitido")]
        [Trait("Category", "Agent Test")]
        public void AgentFactory_CriarDeveRetornarErroSalarioInferiorAoPermitido()
        {
            // Arrange && Act
            Exception exception = Assert.Throws<Exception>(() => AgentFactory.Create("Jackson", 250));

            // Assert
            Assert.Equal("Salário inferior ao permitido", exception.Message);
        }

        [Fact(DisplayName = "CriarDeveRetornarErroSalarioInferiorAoPermitido")]
        [Trait("Category", "Agent Test")]
        public void Agent_Salario_CriarDeveRetornarErroSalarioInferiorAoPermitido()
        {
            // Arrange && Act
            Exception exception = Assert.Throws<Exception>(() => AgentFactory.Create("Jackson", 250));

            // Assert
            Assert.Equal("Salário inferior ao permitido", exception.Message);
        }

        [Fact(DisplayName = "NaoDeveSerNuloOuVazio")]
        [Trait("Category", "Agent Test")]
        public void Agent_Nome_NaoDeveSerNuloOuVazio()
        {
            // Arrange && Act
            Agent agent = AgentFactory.Create("Jackson", 1500);

            // Assert
            Assert.False(string.IsNullOrEmpty(agent.Name));
        }

        [Fact(DisplayName = "NaoDeveTerApelido")]
        [Trait("Category", "Agent Test")]
        public void Agent_Apelido_NaoDeveTerApelido()
        {
            // Arrange && Act
            Agent agent = AgentFactory.Create("Jackson", 1500);

            // Assert
            Assert.True(string.IsNullOrEmpty(agent.Nickname));
        }

        [Fact(DisplayName = "ApelidoDeveSerIgual")]
        [Trait("Category", "Agent Test")]
        public void Agent_Apelido_ApelidoDeveSerIgual()
        {
            // Arrange && Act
            Agent agent = AgentFactory.Create("Jackson", 1500);
            agent.Nickname = "Jack";

            // Assert
            Assert.Equal("Jack", agent.Nickname);
        }
    }
}
