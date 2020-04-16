using Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota : Entity<int>
    {
      

        public Cuota(double valor, DateTime fechaCreacion)
        {
            Valor = valor;
            Estado = Estado.Pendiente;
            FechaCreacion = fechaCreacion;        
           // Abonos = new List<Pagos>();
        }

        public double Valor { get; set; }
        public Estado Estado { get; set; }
        public DateTime FechaCreacion { get; set; }       
        public double ValorPagado { get; set; }

        public double ValorRestantePorPagar() {
            return Valor - ValorPagado;
        }
        //public List<Pagos> Abonos { get; set; }


        
    }

    public enum Estado { 
        Pendiente = 0,
        Pagado = 1
    }
}
