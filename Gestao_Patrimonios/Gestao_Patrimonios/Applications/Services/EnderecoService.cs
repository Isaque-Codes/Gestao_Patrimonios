using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.EnderecoDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Gestao_Patrimonios.Applications.Services
{
    public class EnderecoService
    {
        private readonly IEnderecoRepository _repository;

        public EnderecoService(IEnderecoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarEnderecoDto> Listar()
        {
            List<Endereco> enderecos = _repository.Listar();

            List<ListarEnderecoDto> dtos = enderecos.Select(e => new ListarEnderecoDto
            {
                EnderecoID = e.EnderecoID,
                Logradouro = e.Logradouro,
                Numero = e.Numero,
                Complemento = e.Complemento,
                CEP = e.CEP,
                BairroID = e.BairroID
            }).ToList();

            return dtos;
        }

        public ListarEnderecoDto BuscarPorId(Guid id)
        {
            Endereco endereco = _repository.BuscarPorId(id);

            if (endereco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            ListarEnderecoDto dto = new ListarEnderecoDto
            {
                EnderecoID = endereco.EnderecoID,
                Logradouro = endereco.Logradouro,
                Numero = endereco.Numero,
                Complemento = endereco.Complemento,
                CEP = endereco.CEP,
                BairroID = endereco.BairroID
            };

            return dto;
        }

        public void Adicionar(CriarEnderecoDto dto)
        {
            ValidarNome.Logradouro(dto.Logradouro);

            if (!_repository.BairroExistente(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe.");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(
                dto.Logradouro,
                dto.Numero,
                dto.BairroID
            );

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço com esses dados.");
            }

            Endereco endereco = new Endereco
            {
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Complemento = dto.Complemento,
                BairroID = dto.BairroID
            };

            _repository.Adicionar(endereco);
        }

        public void Atualizar(Guid enderecoId, CriarEnderecoDto dto)
        {
            ValidarNome.Logradouro(dto.Logradouro);

            Endereco enderecoBanco = _repository.BuscarPorId(enderecoId);

            if (enderecoBanco == null)
            {
                throw new DomainException("Endereço não encontrado.");
            }

            if (!_repository.BairroExistente(dto.BairroID))
            {
                throw new DomainException("Bairro informado não existe.");
            }

            Endereco enderecoExistente = _repository.BuscarPorLogradouroENumero(
                dto.Logradouro,
                dto.Numero,
                dto.BairroID,
                enderecoId
            );

            if (enderecoExistente != null)
            {
                throw new DomainException("Já existe um endereço com esses dados.");
            }

            enderecoBanco.Logradouro = dto.Logradouro;
            enderecoBanco.Numero = dto.Numero;
            enderecoBanco.Complemento = dto.Complemento;
            enderecoBanco.BairroID = dto.BairroID;

            _repository.Atualizar(enderecoBanco);
        }
    }
}
