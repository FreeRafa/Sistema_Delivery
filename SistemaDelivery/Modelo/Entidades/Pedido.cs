using System;
using System.Collections.Generic;
using System.Text;
using SistemaDelivery.Modelo.Enums;

namespace SistemaDelivery.Modelo.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public StatusPedido StatusPedido { get; set; } = StatusPedido.Preparado;
        public decimal Total { get; set; }

        public int RestauranteId { get; set; }
        public Restaurante Restaurante { get; set; }

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}
