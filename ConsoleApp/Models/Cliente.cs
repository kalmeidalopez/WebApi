using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    public class Cliente : Persona
    {

        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public virtual List<Cuenta> Cuentas { get; set; }
    }
}