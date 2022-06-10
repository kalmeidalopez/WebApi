using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class Cuenta
    {
        public int IdCuenta { get; set; }
        public int Clienteid { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual List<Movimiento> Movimientos { get; set; }
    }
}
