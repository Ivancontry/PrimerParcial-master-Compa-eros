using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Services.Credito
{
    public class ConsultasCreditoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ConsultasCreditoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Domain.Entities.Credito ConsultarCredito(string codigo)
        {
            return _unitOfWork.CreditoRepository.FindBy(x => x.CodigoCredito == codigo, includeProperties: "Cuotas,Pagos").FirstOrDefault();
        }
        public List<Cuota> ConsultarCuotas(ConsultarCreditoRequest request) {
            var empleado = _unitOfWork.EmpleadoRepository.FindFirstOrDefault(x => x.Cedula == request.Cedula);
            if (empleado != null) {
                var credito = ConsultarCredito(request.CodigoCredito);
                if (credito != null) {
                    return credito.Cuotas;
                }
            }
            return null;
        }
                
        public List<Pago> ConsultarPagos(ConsultarCreditoRequest request)
        {
            Empleado empleado = _unitOfWork.EmpleadoRepository.FindBy(x => x.Cedula == request.Cedula, includeProperties: "Creditos").FirstOrDefault();
            if (empleado != null)
            {
                var credito = ConsultarCredito(request.CodigoCredito);
                if (credito != null)
                {
                    return credito.Pagos;
                }
            }
            return null;
        }
    }

    public class ConsultarCreditoRequest
    {
        public string Cedula { get; set; }
        public string CodigoCredito { get; set; }
       
    }

}
