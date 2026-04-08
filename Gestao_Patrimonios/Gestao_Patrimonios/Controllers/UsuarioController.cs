using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.UsuarioDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize]
        public ActionResult<List<ListarUsuarioDto>> Listar()
        {
            List<ListarUsuarioDto> usuarios = _service.Listar();

            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<ListarUsuarioDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarUsuarioDto usuario = _service.BuscarPorId(id);

                return Ok(usuario);
            }

            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Adicionar(CriarUsuarioDto dto)
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Coordenador")]
        public ActionResult Atualizar(Guid id, CriarUsuarioDto dto)
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

        [HttpPatch("{id}/status")]
        [Authorize(Roles = "Coordenador")]
        public ActionResult AtualizarStatus(Guid id, AtualizarStatusUsuarioDto dto)
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