using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(cliente =>
            {
                cliente.ToTable("tbCliente");
                cliente.HasKey(p => p.Id);
                cliente.Property(p => p.Id).IsRequired().HasColumnName("Clienteid");
                cliente.Property(p => p.Nombre).IsRequired().HasMaxLength(150);

                cliente.Property(p => p.Contrasenia).IsRequired();
                cliente.Property(p => p.Estado).IsRequired();

                cliente.Property(p => p.Direccion).IsRequired();
                cliente.Property(p => p.Telefono).IsRequired();
                cliente.Property(p => p.Identificacion).IsRequired(false);
                cliente.Property(p => p.Edad);
                cliente.Property(p => p.FechaIngreso).IsRequired();
                cliente.Property(p => p.Genero).IsRequired(false);
            });

            modelBuilder.Entity<Cuenta>(cuenta =>
            {
                cuenta.ToTable("tbCuenta");
                cuenta.HasKey(p => p.IdCuenta);
                cuenta.Property(p => p.Clienteid);
                cuenta.HasOne(p => p.Cliente).WithMany(p => p.Cuentas).HasForeignKey(p => p.Clienteid);
                cuenta.Property(p => p.NumeroCuenta).IsRequired();
                cuenta.Property(p => p.TipoCuenta).IsRequired();
                cuenta.Property(p => p.SaldoInicial).IsRequired();
                cuenta.Property(p => p.FechaIngreso).IsRequired();
                cuenta.Property(p => p.Estado).IsRequired();
            });

            modelBuilder.Entity<Movimiento>(movimiento =>
            {
                movimiento.ToTable("tbMovimiento");
                movimiento.HasKey(p => p.IdMovimiento);
                movimiento.Property(p => p.IdCuenta);
                movimiento.HasOne(p => p.Cuenta).WithMany(p => p.Movimientos).HasForeignKey(p => p.IdCuenta);
                movimiento.Property(p => p.Fecha).IsRequired();
                movimiento.Property(p => p.TipoMovimiento).IsRequired();
                movimiento.Property(p => p.Valor).IsRequired();
                movimiento.Property(p => p.Saldo).IsRequired();
            });

          
        }
    }
}
