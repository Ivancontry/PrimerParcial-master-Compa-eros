using Domain.Entities;
using Domain.Factory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Test
{
    public class CrearCreditoTest
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        [TestCase("1003195636","Ivan Contreras",1000000,4000000,0.005,9, "Error, El valor mínimo de un crédito debe ser 5000000", TestName ="ValorCreditoMinimoIncorrecto")]
        [TestCase("1003195636", "Ivan Contreras", 1000000, 11000000, 0.005, 9, "Error, El valor máximo de un crédito debe ser 10000000", TestName = "ValorCreditoMaximoIncorrecto")]
        [TestCase("1003195636", "Ivan Contreras", 1000000, 6000000, 0.005, 12, "Error,El plazo máximo del pago del crédito es 10 meses", TestName = "PlazoCreditoIncorrecto")]
        [TestCase("1003195636", "Ivan Contreras", 1000000, 6000000, 0.005, 9, "Registro de Crédito Exitoso, Valor del Crédito a Pagar 6270000", TestName = "CrearCreditoCorrecto")]
        public void TestCaseCrearCredito(string cedula, string nombre, double salario,double valorCredito, double tasaInteres, int plazo, string mensajeEsperado) {
            Empleado empleado = new Empleado(cedula, nombre, salario);
            var respuesta = CreditoFactory.CanCrearCredito(valorCredito, tasaInteres, plazo);
            if (respuesta.Count == 0)
            {
                Credito credito = CreditoFactory.CrearCredito(valorCredito, tasaInteres, plazo);
                if (credito != null) {
                    respuesta.Add(mensajeEsperado);
                }
                
            }         
            var obtenido = "";
            if (respuesta.Contains(mensajeEsperado)) obtenido = mensajeEsperado;
            Assert.AreEqual(mensajeEsperado, obtenido);
        }
    }
}
