using Infrastructure;
using Infrastructure.Base;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Test
{
    public class CrearEmpleadoServiceTest
    {
        BancoContext _context;

        [SetUp]
        public void Setup()
        {
            /*var optionsSqlServer = new DbContextOptionsBuilder<BancoContext>()
             .UseSqlServer("Server=.\\;Database=Banco;Trusted_Connection=True;MultipleActiveResultSets=true")
             .Options;*/

            var optionsInMemory = new DbContextOptionsBuilder<BancoContext>().UseInMemoryDatabase("Banco").Options;
            _context = new BancoContext(optionsInMemory);
        }

        [Test]
        [TestCase("1003195636","Ivan Contreras",1000000, "Se Registro Correctamente el empleado 1003195636--Ivan Contreras",TestName = "CrearEmpleadoCorrectoTest")]
        [TestCase("1003195636", "Ivan Contreras", 1000000, "El empleado 1003195636--Ivan Contreras. ya esta registrado", TestName = "CrearEmpleadoIncorrectoTest")]
        public void CrearEmpladoTest(string cedula, string nombre, double salario, string mensajeEsperado)
        {
            var request = new CrearEmpleadoRequest { Cedula= cedula, Nombre = nombre, Salario =salario};
            CrearEmpleadoService _service = new CrearEmpleadoService(new UnitOfWork(_context));
            var response = _service.Ejecutar(request);
            Assert.AreEqual(mensajeEsperado, response.Mensaje);
        }
    }
}
