using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Domain.Contracts;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly BancoContext _bancoContext;
        private readonly IUnitOfWork _unitOfWork;
        public EmpleadoController(BancoContext bancoContext, IUnitOfWork unitOfWork)
        {
            _bancoContext = bancoContext;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("")]
        public ActionResult<CrearEmpleadoResponse> Post(CrearEmpleadoRequest request)
        {
            CrearEmpleadoService _service = new CrearEmpleadoService(_unitOfWork);
            CrearEmpleadoResponse response = _service.Ejecutar(request);
            return Ok(response);
        }
    }
}