using NerdStore.Core.DomainObjects;
using System;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoItemTests
    {
        [Fact(DisplayName = "Novo Item Pedido com unidades abaixo do permitido")]
        [Trait("Categoria ", "Pedido Item Tests")]
        public void AdicionarItemPedido_UnidadesItemAbaixoDOPermitido_DeveRetornarException()
        {
            // Arrange & Act & Assert
            Assert.Throws<DomainException>(() => new PedidoItem(Guid.NewGuid(), Pedido.MIN_UNIDADES_ITEM - 1, 100));
        }
    }
}