using System;
using NerdStore.Core.DomainObjects;

namespace NerdStore.Vendas.Domain
{
    public class PedidoItem : Entity
    {
        public Guid PedidoId { get; private set; }

        public Guid ProdutoId { get; private set; }

        public int Quantidade { get; private set; }

        public decimal ValorUnitario { get; private set; }

        public Pedido Pedido { get; set; }

        public PedidoItem(Guid produtoId, int quantidade, decimal valorUnitario)
        {
            if (quantidade < Pedido.MIN_UNIDADES_ITEM)
                throw new DomainException($"Mínimo de {Pedido.MIN_UNIDADES_ITEM} unidades por produto");

            ProdutoId = produtoId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }

        internal void AssociarPedido(Guid pedidoId)
            => PedidoId = pedidoId;

        internal decimal CalcularValor()
            => Quantidade * ValorUnitario;

        internal void AdicionarUnidades(int unidades)
            => Quantidade += unidades;

        internal void AtualizarUnidades(int unidades)
            => Quantidade = unidades;
    }
}