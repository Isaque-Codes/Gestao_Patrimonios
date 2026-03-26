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

        public void Adicionar(CriarLocalizacaoDto Dto)
        {
            Validar.validarNome(Dto.NomeLocal);

            if (!_repository.AreaExistente(Dto.AreaID))
            {
                throw new DomainException("Área informada inexistente.");
            }

            Localizacao localizacao = new Localizacao
            {
                LocalizacaoID = Dto.AreaID,
                NomeLocal = Dto.NomeLocal,
                LocalSAP = Dto.LocalSAP,
                DescricaoSAP = Dto.DescricaoSAP,
                AreaID = Dto.AreaID
            };

            _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid id, CriarLocalizacaoDto dto)
        {
            Validar.validarNome(dto.NomeLocal);

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
