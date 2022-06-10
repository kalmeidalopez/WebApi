using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class Cliente : Persona
    {
        public string Contrasenia { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public List<Cuenta> Cuentas { get; set; }
    }

    public class ClienteInsertDTO : PersonaInsertDTO
    {
        public string Contrasenia { get; set; }
    }
    public class ClienteUpdateDTO : Persona
    {
        public string Contrasenia { get; set; }
    }

}