using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.SolicitacaoTransferenciaDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitacaoTransferenciaController : ControllerBase
    {
        private readonly SolicitacaoTransferenciaService _service;

        public SolicitacaoTransferenciaController(SolicitacaoTransferenciaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarSolicitacaoTransferenciaDto>> Listar()
        {
            return Ok(_service.Listar());
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ListarSolicitacaoTransferenciaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarSolicitacaoTransferenciaDto dto = _service.BuscarPorId(id);

                return Ok(dto);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}