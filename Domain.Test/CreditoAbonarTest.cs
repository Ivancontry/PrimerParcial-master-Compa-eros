using Domain.Entities;
using Domain.Factory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Test
{
    public class CreditoAbonarTest
    {
        public Empleado empleado;
        public Credito credito;

        [SetUp]
        public void Setup()
        {
            empleado = new Empleado("1003195636", "Ivan Contreras", 1000000);
            var respuestaCredito = CreditoFactory.CanCrearCredito(6000000, 0.005, 10);
            if (respuestaCredito.Count == 0)
            {
                credito = CreditoFactory.CrearCredito(6000000, 0.005, 10);               
                if (credito != null) {
                    credito.CodigoCredito = "0001";
                    empleado.Creditos.Add(credito); 
                }
            }
        }
        [Test]
        [TestCase(7000000, "Error, valor a abonar supera al Saldo del Crédito", TestName = "AbonoMayorAlSaldoFaltantePorPagarDelCredito")]
        [TestCase(-5000000, "Error, valor a abonar incorrecto", TestName = "AbonoDelCreditoMenorACero")]
        [TestCase(630000, "Su cuota se registró correctamente por un valor 630000, saldo del crédito restante 5670000", TestName = "RegistroAbonoCorrecto")]
        [TestCase(600000, "Error, El valor mínimo a pagar es de 630000", TestName = "RegistroAbonoIncorrecto")]
        public void TestCaseAbonarCredito(double valorAbonar,string mensajeEsperado)
        {
           
            Credito creditoAbonar = empleado.Creditos.FirstOrDefault(x=> x.CodigoCredito == credito.CodigoCredito);
            var respuesta = creditoAbonar.CanAbonar(valorAbonar);            
            if (respuesta.Count == 0) {
                respuesta.Add(creditoAbonar.Abonar(valorAbonar));
            }
            //"Su cuota se registró correctamente por un valor 630000 ,saldo del crédito restante 5670000"
            var obtenido = "";
            if (respuesta.Contains(mensajeEsperado)) obtenido = mensajeEsperado;
            Assert.AreEqual(mensajeEsperado, obtenido);
        }
                
    }
}
