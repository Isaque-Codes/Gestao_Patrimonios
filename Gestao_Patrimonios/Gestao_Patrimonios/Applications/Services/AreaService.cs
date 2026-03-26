using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.AreaDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class AreaService
    {
        private readonly IAreaRepository _repository;

        public AreaService(IAreaRepository repository)
        {
            _repository = repository;
        }

        public List<ListarAreaDto> Listar()
        {
            List<Area> areas = _repository.Listar();

            List<ListarAreaDto> areasDto = areas.Select(
                a => new ListarAreaDto
                {
                    AreaID = a.AreaID,
                    NomeArea = a.NomeArea
                }).ToList();

            return areasDto;
        }

        public ListarAreaDto BuscarPorId(Guid areaId)
        {
            Area area = _repository.BuscarPorId(areaId);

            if (area == null)
            {
                throw new DomainException("Não existe área com este ID.");
            }

            ListarAreaDto areaDto = new ListarAreaDto
            {
                AreaID = areaId,
                NomeArea = area.NomeArea
            };

            return areaDto;
        }

        public void Adicionar(CriarAreaDto dto)
        {
            Validar.validarNome(dto.NomeArea);

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExistente != null)
            {
                throw new DomainException("Já existe uma cadastrada com este nome.");
            }

            Area area = new Area
            {
                NomeArea = dto.NomeArea
            };

            _repository.Adicionar(area);
        }

        public void Atualizar(Guid areaId, CriarAreaDto dto)
        {
            Validar.validarNome(dto.NomeArea);

            Area areaBanco = _repository.BuscarPorId(areaId);

            if (areaBanco == null)
            {
                throw new DomainException("Área não encontrada.");
            }

            Area areaExistente = _repository.BuscarPorNome(dto.NomeArea);

            if (areaExistente != null)
            {
                throw new DomainException("Já existe uma cadastrada com este nome.");
            }

            areaBanco.NomeArea = dto.NomeArea;

            _repository.Atualizar(areaBanco);
        }
    }
}