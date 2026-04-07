using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.LogPatrimonioDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogPatrimonioController : ControllerBase
    {
        private readonly LogPatrimonioService _service;

        public LogPatrimonioController(LogPatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarLogPatrimonioDto>> Listar()
        {
            List<ListarLogPatrimonioDto> logs = _service.Listar();

            return Ok(logs);
        }

        [HttpGet("patrimonio/{id}")]
        [Authorize]
        public ActionResult<ListarLogPatrimonioDto> BuscarPorPatrimonio(Guid id)
        {
            try
            {
                List<ListarLogPatrimonioDto> logs = _service.BuscarPorPatrimonio(id);

                return Ok(logs);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}