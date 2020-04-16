using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Services.Credito;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly BancoContext _bancoContext;
        private readonly IUnitOfWork _unitOfwork;

        public CreditoController(BancoContext bancoContext, IUnitOfWork unitOfWork)
        {
            _bancoContext = bancoContext;
            _unitOfwork = unitOfWork;
        }

        [HttpPost("")]
        public ActionResult<CrearCreditoResponse> Post(CrearCreditoRequest request)
        {
            CrearCreditoService _service = new CrearCreditoService(_unitOfwork);
            CrearCreditoResponse response = _service.Ejecutar(request);
            return Ok(response);
        }

        [HttpPost("Abonar")]
        public ActionResult<AbonarResponse> Post(AbonarRequest request)
        {
            AbonarService _service = new AbonarService(_unitOfwork);
            AbonarResponse response = _service.Ejecutar(request);
            return Ok(response);
        }

        [HttpGet("ConsultarCuotas")]
        public ActionResult<IEnumerable<Cuota>> GetConsularCuotas(ConsultarCreditoRequest request)
        {
            ConsultasCreditoService _service = new ConsultasCreditoService(_unitOfwork);
             var response = _service.ConsultarCuotas(request);
            return Ok(response);
        }

        [HttpGet("ConsultarPagos")]
        public ActionResult<IEnumerable<Pago>> GetConsultarPagos(ConsultarCreditoRequest request)
        {
            ConsultasCreditoService _service = new ConsultasCreditoService(_unitOfwork);
            var response = _service.ConsultarPagos(request);
            return Ok(response);
        }


    }
}