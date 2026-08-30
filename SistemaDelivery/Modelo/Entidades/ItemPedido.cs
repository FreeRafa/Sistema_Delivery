using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Entidades
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public int PedidoId { get; set; }
        public Pedido pedido { get; set; }

        public int PratoId { get; set; }
        public Prato prato { get; set; }
    }
}
