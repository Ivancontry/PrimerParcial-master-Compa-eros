using Domain.Entities;
using Domain.Factory;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Test
{
    public class CreditoConsultarTest
    {
        public Empleado empleado;
        public Credito credito, creditoAbonar;

        [SetUp]
        public void Setup()
        {
            empleado = new Empleado("1003195636", "Ivan Contreras", 1000000);
            var respuestaCredito = CreditoFactory.CanCrearCredito(6000000, 0.005, 10);
            if (respuestaCredito.Count == 0)
            {
                credito = CreditoFactory.CrearCredito(6000000, 0.005, 10);
                if (credito != null)
                {
                    credito.CodigoCredito = "0001";
                    empleado.Creditos.Add(credito);
                }
            }
            creditoAbonar = empleado.Creditos.FirstOrDefault(x => x.CodigoCredito == credito.CodigoCredito);
        }

        [Test]
        [TestCase(1890000,TestName = "ListaDeCuotasTest")]
        public void TestConsultarCuotas( double valorAbonar)
        {
            creditoAbonar.Abonar(valorAbonar);
            DatosPrueba();
            var lista = creditoAbonar.Cuotas;
            List<string> listaPrueba = new List<string>();
            lista.ForEach(x => { listaPrueba.Add(x.FechaCreacion + " " + x.Valor + " " + x.Estado); });
            Assert.AreEqual(DatosPrueba(), listaPrueba);

        }

        private static List<string> DatosPrueba()
        {
            List<string> listaPrueba = new List<string>();
            listaPrueba.Add(DateTime.Now.Date.AddMonths(1) + " " + 630000 + " Pagado");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(2) + " " + 630000 + " Pagado");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(3) + " " + 630000 + " Pagado");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(4) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(5) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(6) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(7) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(8) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(9) + " " + 630000 + " Pendiente");
            listaPrueba.Add(DateTime.Now.Date.AddMonths(10) + " " + 630000 + " Pendiente");
            return listaPrueba;
        }


        [Test] 
        public void ListaDeAbonosTest()
        {
           
            creditoAbonar.Abonar(1000000);
            creditoAbonar.Abonar(700000);
            creditoAbonar.Abonar(700000);
            DatosPrueba();

            var lista = creditoAbonar.Pagos;
            List<string> listaPruebaPagos = new List<string>();
            lista.ForEach(x => { listaPruebaPagos.Add(x.FechaCreacion + " " + x.Valor); });
            Assert.AreEqual(DatosPruebaPagos(), listaPruebaPagos);

        }

        private static List<string> DatosPruebaPagos()
        {
            List<string> listaPrueba = new List<string>();
            listaPrueba.Add(DateTime.Now.Date + " " + 1000000);
            listaPrueba.Add(DateTime.Now.Date + " " + 700000);
            listaPrueba.Add(DateTime.Now.Date + " " + 700000);
            
            return listaPrueba;
        }


    }
}
