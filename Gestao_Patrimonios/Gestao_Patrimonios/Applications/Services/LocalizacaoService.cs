using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.LocalizacaoDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<ListarLocalizacaoDto> Dtos = localizacoes.Select
                (localizacoes => new ListarLocalizacaoDto
                {
                    LocalizacaoID = localizacoes.LocalizacaoID,
                    NomeLocal = localizacoes.NomeLocal,
                    LocalSAP = localizacoes.LocalSAP,
                    DescricaoSAP = localizacoes.DescricaoSAP,
                    AreaID = localizacoes.AreaID
                }).ToList();

            return Dtos;
        }

        public ListarLocalizacaoDto BuscarPorId(Guid localizacaoId)
        {
            Localizacao localizacao = _repository.BuscarPorId(localizacaoId);

            if (localizacao == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            return new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            };
        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localExistente = _repository.BuscarPorNome(dto.AreaID, dto.NomeLocal);

            if (localExistente != null)
            {
                throw new DomainException("Já existe um local cadastrado com este nome nesta área");
            }

            if (!_repository.AreaExistente(dto.AreaID))
            {
                throw new DomainException("Área informada inexistente.");
            }

            Localizacao localizacao = new Localizacao
            {
                LocalizacaoID = dto.AreaID,
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP,
                DescricaoSAP = dto.DescricaoSAP,
                AreaID = dto.AreaID
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid id, CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localExistente = _repository.BuscarPorNome(dto.AreaID, dto.NomeLocal);

            if (localExistente != null)
            {
                throw new DomainException("Já existe um local cadastrado com este nome nesta área");
            }

            Localizacao localizacaoBanco = _repository.BuscarPorId(id);

            if (localizacaoBanco == null)
            {
                throw new DomainException("Localizacão não encontrada.");
            }

            if (!_repository.AreaExistente(dto.AreaID))
            {
                throw new DomainException("Área informada inexistente");
            }

            localizacaoBanco.NomeLocal = dto.NomeLocal;
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescricaoSAP;
            localizacaoBanco.AreaID = dto.AreaID;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
