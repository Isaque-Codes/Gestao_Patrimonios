using Gestao_Patrimonios.Applications.Autenticacao;
using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.AutenticacaoDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class AutenticacaoService
    {
        private readonly IUsuarioRepository _repository;
        private readonly GeradorTokenJwt _jwt;

        public AutenticacaoService(IUsuarioRepository repository, GeradorTokenJwt jwt)
        {
            _repository = repository;
            _jwt = jwt;
        }

        private static bool VerificarSenha(string senhaDigitada, byte[] senhaHashBanco)
        {
            var hashDigitado = CriptografiaUsuario.CriptografarSenha(senhaDigitada);

            return hashDigitado.SequenceEqual(senhaHashBanco);
        }

        public TokenDto Login(LoginDto dto)
        {
            Usuario usuario = _repository.BuscarPorNIFComTipoUsuario(dto.NIF);

            if (usuario == null)
            {
                throw new DomainException("NIF ou senha inválidos.");
            }

            if (usuario.Ativo == false)
            {
                throw new DomainException("Usuário inativo.");
            }

            if (!VerificarSenha(dto.Senha, usuario.Senha))
            {
                throw new DomainException("NIF ou senha inválidos.");
            }

            string token = _jwt.GerarToken(usuario);

            return new TokenDto
            {
                Token = token,
                PrimeiroAcesso = usuario.PrimeiroAcesso,
                TipoUsuario = usuario.TipoUsuario.NomeTipo
            };
        }

        public void TrocarPrimeiraSenha(Guid usuarioId, TrocarPrimeiraSenhaDto dto)
        {
            ValidarCampo.Senha(dto.SenhaAtual);
            ValidarCampo.Senha(dto.NovaSenha);

            Usuario usuario = _repository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            if (!VerificarSenha(dto.SenhaAtual, usuario.Senha))
            {
                throw new DomainException("Senha atual inválida.");
            }

            if (dto.SenhaAtual == dto.NovaSenha)
            {
                throw new DomainException("A nova senha deve ser diferente da senha atual.");
            }

            usuario.Senha = CriptografiaUsuario.CriptografarSenha(dto.NovaSenha);
            usuario.PrimeiroAcesso = false;

            _repository.AtualizarSenha(usuario);
            _repository.AtualizarPrimeiroAcesso(usuario);
        }
    }
}