using ConsoleApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string endpointClientes = "http://localhost:51229/api/Clientes/";
            string endpointCuentas = "http://localhost:51229/api/Cuentas/";
            string endpointMovimientos = "http://localhost:51229/api/Movimientos/";


            Console.WriteLine("1. Creacion de Cliente.!!");
            List<Cliente> clientes = new List<Cliente>();
            List<Cuenta> cuentas1 = new List<Cuenta>();
            List<Cuenta> cuentas2 = new List<Cuenta>();
            List<Cuenta> cuentas3 = new List<Cuenta>();
            List<Cuenta> cuentas = new List<Cuenta>();
            cuentas1.Add(new Cuenta()
            {
                NumeroCuenta = "478758",
                TipoCuenta = "Ahorro",
                SaldoInicial = 2000
            });
            cuentas2.Add(new Cuenta()
            {
                NumeroCuenta = "225487",
                TipoCuenta = "Corriente",
                SaldoInicial = 100
            });
            cuentas2.Add(new Cuenta()
            {
                NumeroCuenta = "496825",
                TipoCuenta = "Corriente",
                SaldoInicial = 540
            });           
            cuentas3.Add(new Cuenta()
            {
                NumeroCuenta = "495878",
                TipoCuenta = "Ahorro",
                SaldoInicial = 0
            });
            clientes.Add(new Cliente()
            {
                Nombre = "Jose Lema",
                Direccion = "Otavalo sn y principal",
                Edad = 0,
                Identificacion = "",
                Genero = "",
                Telefono = "098254785",
                Contrasenia = "1234",
                Cuentas = cuentas1
            });
            clientes.Add(new Cliente()
            {
                Nombre = "Marianela Montalvo",
                Direccion = "Amazonas y NNUU",
                Edad = 0,
                Identificacion = "",
                Genero = "",
                Telefono = "097548965",
                Contrasenia = "5678",
                Cuentas = cuentas2
            });
            clientes.Add(new Cliente()
            {
                Nombre = "Juan Osorio",
                Direccion = "3 junio y Equinoccial",
                Edad = 0,
                Identificacion = "",
                Genero = "",
                Telefono = "098874587",
                Contrasenia = "1245",
                Cuentas = cuentas3
            });
            foreach (var cliente in clientes)
            {
               var response = http(endpointClientes, cliente);
                if(response != null && response.Id != 0)
                    cliente.Id = response.Id;
            }
            Console.WriteLine("2. Creacion de Cuentas de Cliente.!");
            foreach (var cliente in clientes.Where(p=>p.Id > 0))
            {
                foreach(var cuenta in cliente.Cuentas)
                {
                    cuenta.Clienteid = cliente.Id;
                    var response = http(endpointCuentas, cuenta);
                    if (response != null && response.Id != 0)
                    {
                        cuenta.IdCuenta = response.Id;
                        cuentas.Add(cuenta);
                    }
                }
            }
            Console.WriteLine("3. Crear una nueva Cuenta Corriente para Jose Lema.!");
            var clienteJose = clientes.Where(p => p.Nombre == "Jose Lema").FirstOrDefault();
            if(clienteJose != null)
            {
                Cuenta cuentajose = new Cuenta()
                {
                    Clienteid = clienteJose.Id,
                    NumeroCuenta = "585545",
                    TipoCuenta = "Corriente",
                    SaldoInicial = 1000
                };
                var response = http(endpointCuentas, cuentajose);
                if (response != null && response.Id != 0)
                {
                    cuentajose.IdCuenta = response.Id;
                    clienteJose.Cuentas.Add(cuentajose);
                    cuentas.Add(cuentajose);
                }
            }
            Console.WriteLine("4. Crear movimientos.!");
            List<Movimiento> movimientos = new List<Movimiento>();

            if(cuentas.Where(p => p.NumeroCuenta == "478758").Any())
            {
                movimientos.Add(new Movimiento
                {
                    IdCuenta = cuentas.Where(p => p.NumeroCuenta == "478758").Select(x => x.IdCuenta).FirstOrDefault(),
                    Valor = 575,
                    TipoMovimiento = "Debito"
                });
            }
            if (cuentas.Where(p => p.NumeroCuenta == "225487").Any())
            {
                movimientos.Add(new Movimiento
                {
                    IdCuenta = cuentas.Where(p => p.NumeroCuenta == "225487").Select(x => x.IdCuenta).FirstOrDefault(),
                    Valor = 600,
                    TipoMovimiento = "Credito"
                });
            }
            if (cuentas.Where(p => p.NumeroCuenta == "495878").Any())
            {
                movimientos.Add(new Movimiento
                {
                    IdCuenta = cuentas.Where(p => p.NumeroCuenta == "495878").Select(x => x.IdCuenta).FirstOrDefault(),
                    Valor = 150,
                    TipoMovimiento = "Credito"
                });
            }
            if (cuentas.Where(p => p.NumeroCuenta == "496825").Any())
            {
                movimientos.Add(new Movimiento
                {
                    IdCuenta = cuentas.Where(p => p.NumeroCuenta == "496825").Select(x => x.IdCuenta).FirstOrDefault(),
                    Valor = 540,
                    TipoMovimiento = "Debito"
                });
            }

            foreach (var movimiento in movimientos)
            {
                var response = http(endpointMovimientos, movimiento);
            }
         
        }
        public static ServiceResponse http(string endpoint, object value)
        {
            ServiceResponse serviceResponse = new ServiceResponse();
            StringContent queryString = null;
            var clientejson = JsonConvert.SerializeObject(value, Newtonsoft.Json.Formatting.Indented);
            queryString = new StringContent(clientejson, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    DateTime fecInicio = DateTime.Now;
                    HttpResponseMessage response = client.PostAsync(string.Format("{0}", endpoint), queryString).Result;

                    response.EnsureSuccessStatusCode();
                    string resultContent = response.Content.ReadAsStringAsync().Result;

                    serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(resultContent);
                }
                catch (Exception ex)
                {
                    
                }
            }
            return serviceResponse;
        }

    }
}
