using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;
using NerdStore.WebApp.Tests.Order;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer("NerdStore.WebApp.Tests.Order.PriorityOrderer", "NerdStore.WebApp.Tests.Order")]
    [Collection(nameof(IntegrationWebTestsFixtureCollection))]
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<StartupWebTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixture<StartupWebTests> testsFixture)
            => _testsFixture = testsFixture;

        [Fact(DisplayName = "Realizar cadastro com sucesso"), TestPriority(1)]
        [Trait("Categoria", "Integra��o Web - Usu�rio")]
        public async Task Usuario_RealizarCadastro_DeveExecutarComSucesso()
        {
            // Arrange
            HttpResponseMessage initialResponse = await _testsFixture.Client.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            string antiForgeryToken =
                _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            _testsFixture.GerarUserSenha();

            IDictionary<string, string> formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryFieldName, antiForgeryToken },
                {"Input.Email", _testsFixture.UsuarioEmail },
                {"Input.Password", _testsFixture.UsuarioSenha },
                {"Input.ConfirmPassword", _testsFixture.UsuarioSenha }
            };

            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Register")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            HttpResponseMessage postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            string responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();

            Assert.Contains($"Hello {_testsFixture.UsuarioEmail}", responseString);
        }

        [Fact(DisplayName = "Realizar cadastro senha fraca"), TestPriority(3)]
        [Trait("Categoria", "Integra��o Web - Usu�rio")]
        public async Task Usuario_RealizarCadastroComSenhaFraca_DeveRetornarMensagemDeErro()
        {
            // Arrange
            HttpResponseMessage initialResponse = await _testsFixture.Client.GetAsync("/Identity/Account/Register");
            initialResponse.EnsureSuccessStatusCode();

            string antiForgeryToken =
                _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            _testsFixture.GerarUserSenha();
            const string senhaFraca = "123456";

            IDictionary<string, string> formData = new Dictionary<string, string>
            {
                { _testsFixture.AntiForgeryFieldName, antiForgeryToken },
                {"Input.Email", _testsFixture.UsuarioEmail },
                {"Input.Password", senhaFraca },
                {"Input.ConfirmPassword", senhaFraca }
            };

            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Register")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            HttpResponseMessage postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            string responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains("Passwords must have at least one non alphanumeric character.", responseString);
            Assert.Contains("Passwords must have at least one lowercase (&#x27;a&#x27;-&#x27;z&#x27;).", responseString);
            Assert.Contains("Passwords must have at least one uppercase (&#x27;A&#x27;-&#x27;Z&#x27;).", responseString);
        }

        [Fact(DisplayName = "Realizar login com sucesso"), TestPriority(2)]
        [Trait("Categoria", "Integra��o Web - Usu�rio")]
        public async Task Usuario_RealizarLogin_DeveExecutarComSucesso()
        {
            // Arrange
            HttpResponseMessage initialResponse = await _testsFixture.Client.GetAsync("/Identity/Account/Login");
            initialResponse.EnsureSuccessStatusCode();

            string antiForgeryToken = _testsFixture.ObterAntiForgeryToken(await initialResponse.Content.ReadAsStringAsync());

            IDictionary<string, string> formData = new Dictionary<string, string>
            {
                {_testsFixture.AntiForgeryFieldName, antiForgeryToken},
                {"Input.Email", _testsFixture.UsuarioEmail},
                {"Input.Password", _testsFixture.UsuarioSenha}
            };

            HttpRequestMessage postRequest = new HttpRequestMessage(HttpMethod.Post, "/Identity/Account/Login")
            {
                Content = new FormUrlEncodedContent(formData)
            };

            // Act
            HttpResponseMessage postResponse = await _testsFixture.Client.SendAsync(postRequest);

            // Assert
            string responseString = await postResponse.Content.ReadAsStringAsync();

            postResponse.EnsureSuccessStatusCode();
            Assert.Contains($"Hello {_testsFixture.UsuarioEmail}!", responseString);
        }
    }
}
