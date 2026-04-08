using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.SolicitacaoTransferenciaDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Applications.Services
{
    public class SolicitacaoTransferenciaService
    {
        private readonly ISolicitacaoTransferenciaRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;

        public SolicitacaoTransferenciaService(ISolicitacaoTransferenciaRepository repository, IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
        }

        public List<ListarSolicitacaoTransferenciaDto> Listar()
        {
            List<SolicitacaoTransferencia> solicitacoes = _repository.Listar();

            List<ListarSolicitacaoTransferenciaDto> dtos = solicitacoes.Select(s => new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = s.TransferenciaID,
                PatrimonioID = s.PatrimonioID,
                LocalizacaoID = s.LocalizacaoID,
                Justificativa = s.Justificativa,
                StatusTransferenciaId = s.StatusTransferenciaID,
                UsuarioSolicitanteID = s.UsuarioSolicitanteID,
                DataCriacaoSolicitante = s.DataCriacaoSolicitante,
                UsuarioSolicitadoID = s.UsuarioSolicitadoID ?? Guid.Empty,
                DataResposta = s.DataResposta
            }).ToList();

            return dtos;
        }

        public ListarSolicitacaoTransferenciaDto BuscarPorId(Guid transferenciaId)
        {
            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            ListarSolicitacaoTransferenciaDto dto = new ListarSolicitacaoTransferenciaDto
            {
                TransferenciaID = solicitacao.TransferenciaID,
                PatrimonioID = solicitacao.PatrimonioID,
                LocalizacaoID = solicitacao.LocalizacaoID,
                Justificativa = solicitacao.Justificativa,
                StatusTransferenciaId = solicitacao.StatusTransferenciaID,
                UsuarioSolicitanteID = solicitacao.UsuarioSolicitanteID,
                DataCriacaoSolicitante = solicitacao.DataCriacaoSolicitante,
                UsuarioSolicitadoID = solicitacao.UsuarioSolicitadoID ?? Guid.Empty,
                DataResposta = solicitacao.DataResposta
            };

            return dto;
        }
    }
}
