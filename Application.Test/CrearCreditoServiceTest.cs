using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test
{
    public class CrearCreditoServiceTest
    {

        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/
                      
            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Creditos").Options;
            _context = new BancoContext(optionsInMemory);
            var requestEmpleado = new CrearEmpleadoRequest { Cedula = "1003195636", Nombre = "Ivan Contreras", Salario = 1000000 };
            CrearEmpleadoService _serviceEmpleado = new CrearEmpleadoService(new UnitOfWork(_context));
            var responseEmpleado = _serviceEmpleado.Ejecutar(requestEmpleado);
        }

        [Test]
        [TestCase("1003195636","1111",4000000, 0.005, 9, "Error, El valor mínimo de un crédito debe ser 5000000", TestName = "CrearCrediroValorCreditoMinimoIncorrectoTest")]
        [TestCase("1003195636","1111",11000000, 0.005, 9, "Error, El valor máximo de un crédito debe ser 10000000", TestName = "CrearCreditoValorCreditoMaximoIncorrectoTest")]
        [TestCase("1003195636","1111",6000000, 0.005, 12, "Error,El plazo máximo del pago del crédito es 10 meses", TestName = "CrearCreditoPlazoCreditoIncorrectoTest")]
        [TestCase("1003195636","1111",6000000, 0.005, 9, "Su credito 1111 por valor de 6000000 se registro correctamente", TestName = "CrearCreditoCorrectoTest")]
        [TestCase("1003195632", "1111", 6000000, 0.005, 9, "El empleado 1003195632 no existe", TestName = "CrearCreditoEmpleadoNoExisteTest")]
        public void CaseTestCrearCreditoService(string cedula,string codigo,double valorCredito, double tasaInteres, int plazo, string mensajeEsperado)
        {
            var request = new CrearCreditoRequest { Cedula = cedula, CodigoCredito = codigo, Valor = valorCredito, TasaInteres = tasaInteres, PlazoMeses = plazo };
            var _service = new CrearCreditoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            var obtenido = "";
            if (response.Mensaje.Contains(mensajeEsperado)) obtenido = mensajeEsperado;
            Assert.AreEqual(mensajeEsperado, obtenido);
    
        }
    }
}
