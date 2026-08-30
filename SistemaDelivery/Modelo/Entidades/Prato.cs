using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDelivery.Modelo.Entidades
{
    public class Prato
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Disponivel { get; set; }

        public int RestaurabteId { get; set; }
        public Restaurante Restaurante { get; set; } 
    }
}
