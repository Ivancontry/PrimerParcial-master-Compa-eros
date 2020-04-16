using Domain.Base;
using Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Empleado : Entity<int>
    {
        public Empleado()
        {
            Creditos = new List<Credito>();
        }
        public Empleado(string cedula, string nombre, double salario)
        {
            Cedula = cedula;
            Nombre = nombre;
            Salario = salario;
            Creditos = new List<Credito>();
        }

        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public double Salario { get; set; }
        public List<Credito> Creditos { get; set; }

       
    }
}
