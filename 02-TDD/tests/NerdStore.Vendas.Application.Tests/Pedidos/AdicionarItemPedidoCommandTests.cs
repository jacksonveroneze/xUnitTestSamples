﻿using NerdStore.Vendas.Application.Commands;
using System;
using System.Linq;
using NerdStore.Vendas.Domain;
using Xunit;

namespace NerdStore.Vendas.Application.Tests.Pedidos
{
    public class AdicionarItemPedidoCommandTests
    {
        [Fact(DisplayName = "Adicionar Item Command Válido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaValido_DevePassarNaValidacao()
        {
            // Arrange
            AdicionarItemPedidoCommand pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), "Produto Teste", 2, 100);

            // Act
            bool result = pedidoCommand.EhValido();

            // Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Adicionar Item Command Inválido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_CommandoEstaInvalido_NaoDevePassarNaValidacao()
        {
            // Arrange
            AdicionarItemPedidoCommand pedidoCommand = new AdicionarItemPedidoCommand(Guid.Empty,
                Guid.Empty, "", 0, 0);

            // Act
            bool result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);

            Assert.Contains(AdicionarItemPedidoValidation.IdClienteErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Contains(AdicionarItemPedidoValidation.IdProdutoErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Contains(AdicionarItemPedidoValidation.NomeErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Contains(AdicionarItemPedidoValidation.QtdMinErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));

            Assert.Contains(AdicionarItemPedidoValidation.ValorErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }

        [Fact(DisplayName = "Adicionar Item Command unidades acima do permitido")]
        [Trait("Categoria", "Vendas - Pedido Commands")]
        public void AdicionarItemPedidoCommand_QuantidadeUnidadesSuperiorAoPermitido_NaoDevePassarNaValidacao()
        {
            // Arrange.
            AdicionarItemPedidoCommand pedidoCommand = new AdicionarItemPedidoCommand(Guid.NewGuid(),
                Guid.NewGuid(), "Produto Teste", Pedido.MAX_UNIDADES_ITEM + 1, 100);

            // Act
            bool result = pedidoCommand.EhValido();

            // Assert
            Assert.False(result);
            Assert.Contains(AdicionarItemPedidoValidation.QtdMaxErroMsg,
                pedidoCommand.ValidationResult.Errors.Select(x => x.ErrorMessage));
        }
    }
}