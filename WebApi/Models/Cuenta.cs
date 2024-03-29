﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
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
        public Cliente Cliente { get; set; }
        public List<Movimiento> Movimientos { get; set; }
    }

    public class CuentaInsertDTO
    {
        public int Clienteid { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
    }

    public class CuentaUpdateDTO
    {
        public int IdCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
    }
    public class CuentaQueryDTO
    {
        public int IdCuenta { get; set; }
        public int Clienteid { get; set; }
        public string NumeroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaIngreso { get; set; }
        public Cliente Cliente { get; set; }
    }
}
