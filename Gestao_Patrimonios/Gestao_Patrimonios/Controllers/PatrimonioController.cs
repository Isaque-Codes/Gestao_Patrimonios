using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.PatrimonioDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatrimonioController : ControllerBase
    {
        private readonly PatrimonioService _service;

        public PatrimonioController(PatrimonioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]

        public ActionResult<List<ListarPatrimonioDto>> Listar()
        {
            List<ListarPatrimonioDto> patrimonios = _service.Listar();

            return Ok(patrimonios);
        }

        public ActionResult<ListarPatrimonioDto> BuscarPorId(Guid patrimonioId)
        {
            try
            {
                ListarPatrimonioDto patrimonio = _service.BuscarPorId(patrimonioId);

                return Ok(patrimonio);
            }

            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("importar-csv")]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Adicionar(IFormFile arquivoCsv)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrWhiteSpace(usuarioIdClaim))
                {
                    return Unauthorized("Usuário não autenticado.");
                }

                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.Adicionar(arquivoCsv, usuarioId);

                return Created();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Coordenador")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusPatrimonioDto dto)
        {
            try
            {
                _service.AtualizarStatus(id, dto);

                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}