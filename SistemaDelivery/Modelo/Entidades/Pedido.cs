using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Entidades
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime DataPedido { get; set; }
        public string StatusPedido { get; set; }
        public decimal Total {  get; set; }

        public int RestauranteId { get; set; }
        public Restaurante restaurante { get; set; }

        public int ClienteId { get; set; }
        public Cliente cliente { get; set; }
    }
}
