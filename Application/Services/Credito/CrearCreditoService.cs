using Domain.Contracts;
using Domain.Entities;
using Domain.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public class CrearCreditoService
    {
        public readonly IUnitOfWork _unitOfWork;
        public CrearCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CrearCreditoResponse Ejecutar(CrearCreditoRequest request) {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(x => x.Cedula == request.Cedula);
            if (empleado != null) {
                Credito credito = empleado.Creditos.Find(x => x.CodigoCredito == request.CodigoCredito);
                if (credito == null) {
                    var errores = CreditoFactory.CanCrearCredito(request.Valor, request.TasaInteres, request.PlazoMeses);
                    if (errores.Count == 0)
                    {
                        Credito creditoNuevo = CreditoFactory.CrearCredito(request.Valor, request.TasaInteres, request.PlazoMeses);
                        creditoNuevo.CodigoCredito = request.CodigoCredito;
                        empleado.Creditos.Add(creditoNuevo);
                        _unitOfWork.EmpleadoRepository.Edit(empleado);
                       // _unitOfWork.CreditoRepository.Add(creditoNuevo);
                        _unitOfWork.Commit();
                        return new CrearCreditoResponse() { Mensaje = $"Su credito {creditoNuevo.CodigoCredito} por valor de {creditoNuevo.Valor} se registro correctamente" };
                    }
                    return new CrearCreditoResponse() { Mensaje = string.Join("-", errores) };
                }
                else {
                    return new CrearCreditoResponse() { Mensaje = $"ya existe un credito registrado con este numero {request.CodigoCredito}" };
                }
            }
            else {
                return new CrearCreditoResponse() { Mensaje = $"El empleado {request.Cedula} no existe" };
            }

        }
        
    }

    public class CrearCreditoRequest {
        public string Cedula { get; set; }
        public string CodigoCredito { get; set; }        
        public double Valor { get; set; }
        public int PlazoMeses { get; set; }
        public double TasaInteres { get; set; }
    }

    public class CrearCreditoResponse { 
        public string Mensaje { get; set; }
    }
}
