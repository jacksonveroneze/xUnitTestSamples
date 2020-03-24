using Xunit;

namespace xUnitTestSamples.Basic.Tests
{
    public class StringsToolsTests
    {

        [Fact(DisplayName = "Deve retornar nome completo")]
        [Trait("Category", "StringTools Test")]
        public void StringTools_UnirNomes_DeveRetornarNomeCompleto()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.Equal("Jackson Veroneze", nomeCompleto);
        }

        [Fact(DisplayName = "Name completo não deve ser igual")]
        [Trait("Category", "StringTools Test")]
        public void StringTools_UnirNomes_NaoDeveSerIgualNomeCompleto()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.NotEqual("Jackson Veronezee", nomeCompleto);
        }

        [Fact(DisplayName = "Name completo deve validar ignorando case")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_DeveIgnorarCase()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("JACKSON", "VERONEZE");

            // Assert
            Assert.Equal("Jackson Veroneze", nomeCompleto, true);
        }

        [Fact(DisplayName = "Name completo deve conter trecho de texto")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.Contains("cks", nomeCompleto);
        }

        [Fact(DisplayName = "Name completo não deve conter trecho de texto")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_NaoDeveConterTrecho()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.DoesNotContain("xxx", nomeCompleto);
        }

        [Fact(DisplayName = "Name completo deve começar com")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.StartsWith("Jac", nomeCompleto);
        }

        [Fact(DisplayName = "Name completo deve terminar com")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_DeveTerminarCom()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.EndsWith("ze", nomeCompleto);
        }

        [Fact(DisplayName = "Valida nome completo com expressão regular")]
        [Trait("Category", "StringTools Test")]
        public void StringsTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            StringsTools stringsTools = new StringsTools();

            // Act
            string nomeCompleto = stringsTools.Join("Jackson", "Veroneze");

            // Assert
            Assert.Matches(@"\w+\s{1}\w+", nomeCompleto);
        }
    }
}
