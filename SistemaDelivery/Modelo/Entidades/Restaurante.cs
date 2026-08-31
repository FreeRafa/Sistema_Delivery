using System;
using System.Collections.Generic;
using System.Text;
using SistemaDelivery.Modelo.Enums;

namespace SistemaDelivery.Modelo.Entidades
{
    public class Restaurante
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nipc { get; set; } = string.Empty;
        public string Telemovel { get; set; } = string.Empty;
        public CategoriaRestaurante Categoria { get; set; }
        public bool Ativo {  get; set; }

        public ICollection<Prato> Prato { get; set; } = new List<Prato>();
        public ICollection<Pedido> Pedido { get; set; } = new List<Pedido>();
    }
}
