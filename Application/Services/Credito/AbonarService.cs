using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class AbonarService
    {
        public readonly IUnitOfWork _unitOfWork;
        public AbonarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public AbonarResponse Ejecutar(AbonarRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindBy(x => x.Cedula == request.Cedula, includeProperties : "Creditos").FirstOrDefault();
            if (empleado != null)
            {
                Credito credito = _unitOfWork.CreditoRepository.FindBy(x => x.CodigoCredito == request.CodigoCredito, includeProperties: "Cuotas,Pagos").FirstOrDefault();
                if (credito == null) return new AbonarResponse() { Mensaje = $"El credito {request.CodigoCredito} no existe" };
                
                var errores = credito.CanAbonar(request.Valor);
                if (errores.Count == 0)
                {
                    var respuesta = credito.Abonar(request.Valor);
                    _unitOfWork.EmpleadoRepository.Edit(empleado);
                    _unitOfWork.Commit();
                    return new AbonarResponse() { Mensaje = respuesta };
                }
                return new AbonarResponse() { Mensaje = string.Join("-", errores) };
                
            }
            else
            {
                return new AbonarResponse() { Mensaje = $"El empleado {request.Cedula} no existe" };
            }
        }

    }
    public class AbonarRequest {
        public string Cedula { get; set; }
        public string CodigoCredito { get; set; }
        public double Valor { get; set; }
       
    }
    public class AbonarResponse {
        public string Mensaje { get; set; }
    }
}
