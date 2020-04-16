using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pago : Entity<int>
    {
        public Pago(double valor)
        {
            Valor = valor;
            FechaCreacion = DateTime.Now.Date;
        }
        public double Valor { get; set; }
        public DateTime FechaCreacion { get; set; }

    }
}
