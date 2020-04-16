using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test
{
    public class AbonarServiceTest
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

            //Crear Empleado
            var requestEmpleado = new CrearEmpleadoRequest { Cedula = "1003195636", Nombre = "Ivan Contreras", Salario = 1000000 };
            CrearEmpleadoService _serviceEmpleado = new CrearEmpleadoService(new UnitOfWork(_context));
            var responseEmpleado = _serviceEmpleado.Ejecutar(requestEmpleado);
            //Crear Credito
            var requestCredito = new CrearCreditoRequest { Cedula = "1003195636", CodigoCredito = "0001", Valor = 6000000, TasaInteres = 0.005, PlazoMeses = 10 };
            var _serviceCredito = new CrearCreditoService(new UnitOfWork(_context));
            var responseCredito = _serviceCredito.Ejecutar(requestCredito);

        }

        [Test]
        [TestCase("1003195636", "0001",7000000, "Error, valor a abonar supera al Saldo del Crédito", TestName = "AbonoMayorAlSaldoFaltantePorPagarDelCredito")]
        [TestCase("1003195636", "0001",-5000000, "Error, valor a abonar incorrecto", TestName = "AbonoDelCreditoMenorACero")]
        [TestCase("1003195636", "0001",630000, "Su cuota se registró correctamente por un valor 630000, saldo del crédito restante 5670000", TestName = "RegistroAbonoCorrecto")]
        [TestCase("1003195636", "0001", 600000, "Error, El valor mínimo a pagar es de 630000", TestName = "RegistroAbonoIncorrecto")]
        [TestCase("1003195631", "0001", 7000000, "El empleado 1003195631 no existe", TestName = "EmpleadoAbonarNoExiste")]
        [TestCase("1003195636", "0002", 7000000, "El credito 0002 no existe", TestName = "CreditoAbonarNoExiste")]
        public void CaseTestCrearCreditoService(string cedula, string codigo, double valorAbonar,string mensajeEsperado)
        {
            var request = new AbonarRequest { Cedula = cedula, CodigoCredito = codigo, Valor = valorAbonar};
            var _service = new AbonarService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            var obtenido = "";
            if (response.Mensaje.Contains(mensajeEsperado)) obtenido = mensajeEsperado;
            Assert.AreEqual(mensajeEsperado, obtenido);
        }
    }
}
