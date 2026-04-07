using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.LogPatrimonioDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class LogPatrimonioService
    {
        private readonly ILogPatrimonioRepository _repository;

        public LogPatrimonioService(ILogPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLogPatrimonioDto> Listar()
        {
            List<LogPatrimonio> logs = _repository.Listar();

            List<ListarLogPatrimonioDto> dto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioID = log.LogPatrimonioID,
                PatrimonioID = log.PatrimonioID,
                DataTransferencia = log.DataTransferencia,
                Localizacao = log.Localizacao.NomeLocal,
                Usuario = log.Usuario.Nome,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao
            }).ToList();

            return dto;
        }

        public List<ListarLogPatrimonioDto> BuscarPorPatrimonio(Guid patrimonioId)
        {
            List<LogPatrimonio> logs = _repository.BuscarPorPatrimonio(patrimonioId);

            if (logs == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            List<ListarLogPatrimonioDto> dto = logs.Select(log => new ListarLogPatrimonioDto
            {
                LogPatrimonioID = log.LogPatrimonioID,
                PatrimonioID = log.PatrimonioID,
                DataTransferencia = log.DataTransferencia,
                Localizacao = log.Localizacao.NomeLocal,
                Usuario = log.Usuario.Nome,
                TipoAlteracao = log.TipoAlteracao.NomeTipo,
                StatusPatrimonio = log.StatusPatrimonio.NomeStatus,
                DenominacaoPatrimonio = log.Patrimonio.Denominacao
            }).ToList();

            return dto;
        }
    }
}
