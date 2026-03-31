using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Gestao_Patrimonios.DTOs.BairroDto;
using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Exceptions;
using GestaoPatrimonios.DTOs.CidadeDto;
using Gestao_Patrimonios.Applications.Regras;

namespace Gestao_Patrimonios.Applications.Services
{
    public class BairroService
    {
        private readonly IBairroRepository _repository;

        public BairroService(IBairroRepository repository)
        {
            _repository = repository;
        }

        public List<ListarBairroDto> Listar()
        {
            List<Bairro> bairros = _repository.Listar();

            List<ListarBairroDto> Dtos = bairros.Select
                (bairro => new ListarBairroDto
                {
                    BairroID = bairro.BairroID,
                    NomeBairro = bairro.NomeBairro,
                    Cidade = bairro.Cidade
                }
            ).ToList();

            return Dtos;
        }

        public ListarBairroDto BuscarPorId(Guid id)
        {
            Bairro bairro = _repository.BuscarPorId(id);

            if (bairro == null)
            {
                throw new DomainException("Não existe bairro com este ID.");
            }

            ListarBairroDto dto = new ListarBairroDto
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                Cidade = bairro.Cidade
            };

            return dto;
        }

        public ListarBairroDto BuscarPorNome(string nome)
        {
            Bairro bairro = _repository.BuscarPorNome(nome);

            if (bairro == null)
            {
                throw new DomainException("Não existe bairro com este nome.");
            }

            ListarBairroDto dto = new ListarBairroDto
            {
                BairroID = bairro.BairroID,
                NomeBairro = bairro.NomeBairro,
                Cidade = bairro.Cidade
            };

            return dto;
        }

        public void Adicionar(Bairro bairro)
        {
            Validar.ValidarBairro(bairro.NomeBairro);
        }
    }
}
