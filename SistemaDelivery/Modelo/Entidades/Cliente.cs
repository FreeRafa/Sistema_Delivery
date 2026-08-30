using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace SistemaDelivery.Modelo.Entidades
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Nif { get; set; } = string.Empty;
        public string Telemovel {  get; set; } = string.Empty;
        public string Email {  get; set; } = string.Empty;
    }
}
