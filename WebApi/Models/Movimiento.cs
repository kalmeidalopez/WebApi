using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Movimiento
    {
        public int IdMovimiento { get; set; }
        public int IdCuenta { get; set; }
        public DateTime Fecha { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public virtual Cuenta Cuenta { get; set; }
    }

    public class MovimientoInsertDTO
    {
        public int IdCuenta { get; set; }
        public string TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
    }
}
