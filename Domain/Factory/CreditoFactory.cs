using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Factory
{
    public class CreditoFactory
    {
        public const int PLAZOMAXIMOENMESES = 10;
        public const double VALORMINIMODELCREDITO = 5000000;
        public const double VALORMAXIMODELCREDITO = 10000000;
      
        public static IList<string> CanCrearCredito( double valor, double tasa, int plazoMeses)
        {
            var errors = new List<string>();
            if (valor < VALORMINIMODELCREDITO) errors.Add("Error, El valor mínimo de un crédito debe ser 5000000");
            if (valor > VALORMAXIMODELCREDITO) errors.Add("Error, El valor máximo de un crédito debe ser 10000000");      
            if (plazoMeses < 0 || plazoMeses > PLAZOMAXIMOENMESES) errors.Add("Error,El plazo máximo del pago del crédito es 10 meses");
            return errors;
        }

        public static Credito CrearCredito( double valor, double tasa, int plazoMeses)
        {
            if (CanCrearCredito(valor,tasa,plazoMeses).Count != 0) { throw new InvalidOperationException(); }
            return new Credito(valor, tasa, plazoMeses);
        }

    }
}
