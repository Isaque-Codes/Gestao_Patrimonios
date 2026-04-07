using Gestao_Patrimonios.Applications.Services;
using Gestao_Patrimonios.DTOs.AutenticacaoDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace Gestao_Patrimonios.Applications.Autenticacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public ActionResult<TokenDto> Login(LoginDto dto)
        {
            try
            {
                TokenDto token = _service.Login(dto);

                return Ok(token);
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPatch("trocar-senha")]
        public ActionResult TrocarPrimeiraSenha(TrocarPrimeiraSenhaDto dto)
        {
            try
            {
                string usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (string.IsNullOrEmpty(usuarioIdClaim))
                {
                    return BadRequest("Usuário não autenticado.");
                }

                // Converte string para GUID
                Guid usuarioId = Guid.Parse(usuarioIdClaim);

                _service.TrocarPrimeiraSenha(usuarioId, dto);

                return NoContent();
            }

            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}