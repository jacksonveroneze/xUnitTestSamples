using System;
using System.Diagnostics;
using System.Linq;
using FluentValidation.Results;
using NerdStore.Core.DomainObjects;
using Xunit;

namespace NerdStore.Vendas.Domain.Tests
{
    public class PedidoTests
    {
        [Fact(DisplayName = "Adicionar Item Novo Pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_NovoPedido_DeveAtualizarValor()
        {
            // Arrange
            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem = new PedidoItem(Guid.NewGuid(), 2, 100);

            // Act
            pedido.AdicionarItem(pedidoItem);

            // Assert
            Assert.Equal(200, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Existente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistente_DeveIncrementarUnidadeSomarValores()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            PedidoItem pedidoItem1 = new PedidoItem(produtoId, 2, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 100);

            pedido.AdicionarItem(pedidoItem1);

            // Act
            pedido.AdicionarItem(pedidoItem2);

            // Assert
            Assert.Equal(300, pedido.ValorTotal);
            Assert.Equal(1, pedido.PedidoItems.Count);
            Assert.Equal(3, pedido.PedidoItems.FirstOrDefault(x => x.ProdutoId == produtoId).Quantidade);
        }

        [Fact(DisplayName = "Adicionar Item Pedido Acima de 15")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_QuantidadeAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            PedidoItem pedidoItem = new PedidoItem(produtoId, Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem));
        }


        [Fact(DisplayName = "Adicionar Item Pedido Acima de 15")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AdicionarItemPedido_ItemExistenteSomaQuantidadeAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            PedidoItem pedidoItem1 = new PedidoItem(produtoId, 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, Pedido.MAX_UNIDADES_ITEM, 100);

            pedido.AdicionarItem(pedidoItem1);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AdicionarItem(pedidoItem2));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem = new PedidoItem(produtoId, 1, 100);

            // Act & Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItem));
        }

        [Fact(DisplayName = "Atualizar Item Pedido Valido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemValido_DeveAtualizarQuantidade()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(produtoId, 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 200);

            int novaQuantidade = pedidoItem2.Quantidade;

            pedido.AdicionarItem(pedidoItem1);

            // Act
            pedido.AtualizarItem(pedidoItem2);

            // Assert
            Assert.Equal(novaQuantidade, pedido.PedidoItems.FirstOrDefault(x => x.ProdutoId == produtoId)?.Quantidade);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Validar Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_PedidoComProdutosDiferentes_DeveAtualizarValorTotal()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 200);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            PedidoItem pedidoItem2Atualizado = new PedidoItem(produtoId, 2, 200);

            decimal valorTotal = pedidoItem1.Quantidade * pedidoItem1.ValorUnitario +
                                 pedidoItem2Atualizado.Quantidade * pedidoItem2Atualizado.ValorUnitario;

            // Act
            pedido.AtualizarItem(pedidoItem2Atualizado);

            // Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Atualizar Item Pedido Quantidade acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AtualizarItemPedido_ItemUnidadesAcimaDoPermitido_DeveRetornarException()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem = new PedidoItem(produtoId, 1, 100);

            pedido.AdicionarItem(pedidoItem);

            PedidoItem pedidoItemAtualizado = new PedidoItem(produtoId, Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act && Assert
            Assert.Throws<DomainException>(() => pedido.AtualizarItem(pedidoItemAtualizado));
        }

        [Fact(DisplayName = "Remover Item Pedido Inexistente")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemNaoExisteNaLista_DeveRetornarException()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem = new PedidoItem(produtoId, 1, 100);

            // Act && Assert
            Assert.Throws<DomainException>(() => pedido.RemoverItem(pedidoItem));
        }

        [Fact(DisplayName = "Remover Item Pedido Deve Calcular Valor Total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void RemoverItemPedido_ItemExistente_DeveAtualizarValorTotal()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 200);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            decimal valorTotal = pedidoItem1.Quantidade * pedidoItem1.ValorUnitario;

            // Act
            pedido.RemoverItem(pedidoItem2);

            // Assert
            Assert.Equal(valorTotal, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher válido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherValido_DeveRetornarSemErros()
        {
            // Arrange
            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("PROMO-15-REAIS", null, 15, 1,
                TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            // Act
            ValidationResult result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar voucher Inválido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void Pedido_AplicarVoucherInvalido_DeveRetornarComErros()
        {
            // Arrange
            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("", null, null, 0,
                TipoDescontoVoucher.Valor, DateTime.Now.AddDays(-1), false, true);

            // Act
            ValidationResult result = pedido.AplicarVoucher(voucher);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "Aplicar voucher tipo valor desconto")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoValorDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 200);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("PROMO-15-REAIS", null, 15, 1,
                TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            decimal valorDesconto = pedido.ValorTotal - (decimal)voucher?.ValorDesconto;

            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(valorDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher tipo percentual desconto")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_VoucherTipoPercentualDesconto_DeveDescontarDoValorTotal()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(produtoId, 1, 200);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("PROMO-15-REAIS", 10, null, 1,
                TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);

            decimal? valorDesconto = pedido.ValorTotal - (pedido.ValorTotal * voucher.PercentualDesconto) / 100;

            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(valorDesconto, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher desconto excede valor total")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_DescontoExcedeValorTotalPedido_PedidoDeveTerValorZero()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);

            pedido.AdicionarItem(pedidoItem1);

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("PROMO-15-REAIS", 1000, null, 1,
                TipoDescontoVoucher.Porcentagem, DateTime.Now.AddDays(15), true, false);

            // Act
            pedido.AplicarVoucher(voucher);

            // Assert
            Assert.Equal(0, pedido.ValorTotal);
        }

        [Fact(DisplayName = "Aplicar voucher recalcular desconto na modificação do pedido")]
        [Trait("Categoria", "Vendas - Pedido")]
        public void AplicarVoucher_ModificarItensPedido_DeveCalcularDescontoValorTotal()
        {
            // Arrange
            Guid produtoId = Guid.NewGuid();

            Pedido pedido = Pedido.PedidoFactory.NovoPedidoRascunho(Guid.NewGuid());
            PedidoItem pedidoItem1 = new PedidoItem(Guid.NewGuid(), 1, 100);
            PedidoItem pedidoItem2 = new PedidoItem(Guid.NewGuid(), 1, 200);

            pedido.AdicionarItem(pedidoItem1);
            pedido.AdicionarItem(pedidoItem2);

            Voucher voucher = Voucher.VoucherFactory.NovoVoucher("PROMO-15-REAIS", null, 100, 1,
                TipoDescontoVoucher.Valor, DateTime.Now.AddDays(15), true, false);

            pedido.AplicarVoucher(voucher);

            PedidoItem pedidoItem3 = new PedidoItem(Guid.NewGuid(), 1, 200);

            // Act
            pedido.AdicionarItem(pedidoItem3);

            // Assert
            decimal totalEsperado = (decimal)(pedido.PedidoItems.Sum(x => x.Quantidade * x.ValorUnitario) - voucher.ValorDesconto);

            Assert.Equal(totalEsperado, pedido.ValorTotal);
        }
    }
}
