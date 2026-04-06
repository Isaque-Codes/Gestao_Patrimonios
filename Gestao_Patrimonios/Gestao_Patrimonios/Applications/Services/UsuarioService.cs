using Gestao_Patrimonios.Applications.Autenticacao;
using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.UsuarioDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<ListarUsuarioDto> dtos = usuarios.Select(u => new ListarUsuarioDto
            {
                UsuarioID = u.UsuarioID,
                NIF = u.NIF,
                Nome = u.Nome,
                RG = u.RG,
                CPF = u.CPF,
                CarteiraTrabalho = u.CarteiraTrabalho,
                Email = u.Email,
                Ativo = u.Ativo,
                PrimeiroAcesso = u.PrimeiroAcesso,
                EnderecoID = u.EnderecoID,
                CargoID = u.CargoID,
                TipoUsuarioID = u.TipoUsuarioID
            }).ToList();

            return dtos;
        }

        public ListarUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario u = _repository.BuscarPorId(usuarioId);

            if (u == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            ListarUsuarioDto dto = new ListarUsuarioDto
            {
                UsuarioID = u.UsuarioID,
                NIF = u.NIF,
                Nome = u.Nome,
                RG = u.RG,
                CPF = u.CPF,
                CarteiraTrabalho = u.CarteiraTrabalho,
                Email = u.Email,
                Ativo = u.Ativo,
                PrimeiroAcesso = u.PrimeiroAcesso,
                EnderecoID = u.EnderecoID,
                CargoID = u.CargoID,
                TipoUsuarioID = u.TipoUsuarioID
            };

            return dto;
        }

        public void Adicionar(CriarUsuarioDto dto)
        {
            ValidarCampo.Nome(dto.Nome);
            ValidarCampo.NIF(dto.NIF);
            ValidarCampo.CPF(dto.CPF);
            ValidarCampo.Email(dto.Email);

            Usuario usuarioDuplicado = _repository.BuscarDuplicata(dto.NIF, dto.CPF, dto.Email);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com este NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com este CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com este email.");
                }
            }

            if (!_repository.EnderecoExistente(dto.EnderecoID))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExistente(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExistente(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            Usuario usuario = new Usuario
            {
                NIF = dto.NIF,
                Nome = dto.Nome,
                RG = dto.RG,
                CPF = dto.CPF,
                CarteiraTrabalho = dto.CarteiraTrabalho,
                Senha = CriptografiaUsuario.CriptografarSenha(dto.NIF),
                Email = dto.Email,
                Ativo = true,
                PrimeiroAcesso = true,
                EnderecoID = dto.EnderecoID,
                CargoID = dto.CargoID,
                TipoUsuarioID = dto.TipoUsuarioID
            };

            _repository.Adicionar(usuario);
        }

        public void Atualizar(Guid usuarioId, CriarUsuarioDto dto)
        {
            ValidarCampo.Nome(dto.Nome);
            ValidarCampo.NIF(dto.NIF);
            ValidarCampo.CPF(dto.CPF);
            ValidarCampo.Email(dto.Email);

            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Usuario usuarioDuplicado = _repository.BuscarDuplicata(dto.NIF, dto.CPF, dto.Email, usuarioId);

            if (usuarioDuplicado != null)
            {
                if (usuarioDuplicado.NIF == dto.NIF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com este NIF.");
                }

                if (usuarioDuplicado.CPF == dto.CPF)
                {
                    throw new DomainException("Já existe um usuário cadastrado com este CPF.");
                }

                if (usuarioDuplicado.Email.ToLower() == dto.Email.ToLower())
                {
                    throw new DomainException("Já existe um usuário cadastrado com este email.");
                }
            }

            if (!_repository.EnderecoExistente(dto.EnderecoID))
            {
                throw new DomainException("Endereço informado não existe.");
            }

            if (!_repository.CargoExistente(dto.CargoID))
            {
                throw new DomainException("Cargo informado não existe.");
            }

            if (!_repository.TipoUsuarioExistente(dto.TipoUsuarioID))
            {
                throw new DomainException("Tipo de usuário informado não existe.");
            }

            usuarioBanco.NIF = dto.NIF;
            usuarioBanco.Nome = dto.Nome;
            usuarioBanco.RG = dto.RG;
            usuarioBanco.CPF = dto.CPF;
            usuarioBanco.CarteiraTrabalho = dto.CarteiraTrabalho;
            usuarioBanco.Email = dto.Email;
            usuarioBanco.EnderecoID = dto.EnderecoID;
            usuarioBanco.CargoID = dto.CargoID;
            usuarioBanco.TipoUsuarioID = dto.TipoUsuarioID;

            _repository.Atualizar(usuarioBanco);
        }

        public void AtualizarStatus(Guid usuarioId, AtualizarStatusUsuarioDto dto)
        {
            Usuario usuarioBanco = _repository.BuscarPorId(usuarioId);

            if (usuarioBanco == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            usuarioBanco.Ativo = dto.Ativo;

            _repository.AtualizarStatus(usuarioBanco);
        }
    }
}