using Domain.Entities;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class BancoContext : DbContextBase
    {
        public BancoContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Credito> Credito { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Cuota> Cuota { get; set; }
        public DbSet<Pago> Pago { get; set; }


    }
}
