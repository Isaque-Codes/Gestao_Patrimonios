using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.CargoDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargoController : ControllerBase
    {
        private readonly CargoService _service;

        public CargoController(CargoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarCargoDto>> Listar()
        {
            List<ListarCargoDto> cargos = _service.Listar();

            return Ok(cargos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ListarCargoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarCargoDto cargo = _service.BuscarPorId(id);

                return Ok(cargo);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Adicionar(CriarCargoDto dto)
        {
            try
            {
                _service.Adicionar(dto);

                return Created();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Atualizar(Guid id, CriarCargoDto dto)
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
