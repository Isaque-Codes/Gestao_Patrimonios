using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.AreaDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private readonly AreaService _service;

        public AreaController(AreaService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarAreaDto>> Listar()
        {
            List<ListarAreaDto> areas = _service.Listar();

            return Ok(areas);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarAreaDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarAreaDto area = _service.BuscarPorId(id);

                return Ok(area);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarAreaDto dto)
        {
            try
            {
                _service.Adicionar(dto);

                return StatusCode(201);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarAreaDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);

                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
