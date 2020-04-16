using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearEmpleadoService
    {
        public readonly IUnitOfWork _unitOfWork;

        public CrearEmpleadoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CrearEmpleadoResponse Ejecutar(CrearEmpleadoRequest request) {

            Empleado empleado= _unitOfWork.EmpleadoRepository.FindFirstOrDefault(x=> x.Cedula == request.Cedula);
            if (empleado == null) {
                Empleado empleadoNuevo = new Empleado();
                empleadoNuevo.Cedula = request.Cedula;
                empleadoNuevo.Nombre = request.Nombre;
                empleadoNuevo.Salario = request.Salario;
                _unitOfWork.EmpleadoRepository.Add(empleadoNuevo);
                _unitOfWork.Commit();
                return new CrearEmpleadoResponse() { Mensaje = $"Se Registro Correctamente el empleado {empleadoNuevo.Cedula}--{empleadoNuevo.Nombre}" };
            }
            else
            {
                return new CrearEmpleadoResponse() { Mensaje = $"El empleado {empleado.Cedula}--{empleado.Nombre}. ya esta registrado" };
            }           
            
        }

    }

    public class CrearEmpleadoRequest {
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public double Salario { get; set; }
        public List<Credito> Creditos { get; set; }
    }

    public class CrearEmpleadoResponse { 
        public string Mensaje { get; set; }
    }
}
