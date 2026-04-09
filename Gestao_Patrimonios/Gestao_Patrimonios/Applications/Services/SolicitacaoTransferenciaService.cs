using Gestao_Patrimonios.Applications.Regras;
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

        public void Adicionar(Guid usuarioId, CriarSolicitacaoTransferenciaDto dto)
        {
            ValidarCampo.Justificativa(dto.Justificativa);

            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(dto.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.LocalizacaoExistente(dto.LocalizacaoID))
            {
                throw new DomainException("Localização de destino inexistente.");
            }

            if (patrimonio.LocalizacaoID == dto.LocalizacaoID)
            {
                throw new DomainException("O patrimônio já está nesta localização.");
            }

            if (_repository.SolicitacaoPendenteExistente(dto.PatrimonioID))
            {
                throw new DomainException("Já existe uma solicitação de transferência pendente para este patrimônio.");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.ResponsavelPeloLocal(usuarioId, patrimonio.LocalizacaoID);

                if (!usuarioResponsavel)
                {
                    throw new DomainException("O responsável só pode solicitar transferência do patrimônio do ambiente ao qual está vinculado.");
                }
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência 'pendente' não encontrado.");
            }

            SolicitacaoTransferencia novaSolicitacao = new SolicitacaoTransferencia
            {
                DataCriacaoSolicitante = DateTime.UtcNow,
                Justificativa = dto.Justificativa,
                StatusTransferenciaID = statusPendente.StatusTransferenciaID,
                UsuarioSolicitanteID = usuarioId,
                UsuarioSolicitadoID = null,
                PatrimonioID = dto.PatrimonioID,
                LocalizacaoID = dto.LocalizacaoID
            };
        }

        public void Responder(Guid transferenciaId, Guid usuarioId, ResponderSolicitacaoTransferenciaDto dto)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(usuarioId);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            SolicitacaoTransferencia solicitacao = _repository.BuscarPorId(transferenciaId);

            if (solicitacao == null)
            {
                throw new DomainException("Solicitação de transferência não encontrada.");
            }

            Patrimonio patrimonio = _repository.BuscarPatrimonioPorId(solicitacao.PatrimonioID);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            StatusTransferencia statusPendente = _repository.BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                throw new DomainException("Status de transferência 'pendente' não encontrado.");
            }

            if (solicitacao.StatusTransferenciaID != statusPendente.StatusTransferenciaID)
            {
                throw new DomainException("Esta solicitação já foi respondida.");
            }

            if (usuario.TipoUsuario.NomeTipo == "Responsável")
            {
                bool usuarioResponsavel = _repository.ResponsavelPeloLocal(usuarioId, patrimonio.LocalizacaoID);
                if (!usuarioResponsavel)
                {
                    throw new DomainException("Somente o responsável pela localização pode aprovar ou rejeitar solicitações.");
                }
            }

            StatusTransferencia statusResposta;

            if (dto.Aprovado)
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Aprovado");
            }
            else
            {
                statusResposta = _repository.BuscarStatusTransferenciaPorNome("Rejeitado");
            }

            if (statusResposta == null)
            {
                throw new DomainException("Status de resposta da transferência não encontrado.");
            }

            solicitacao.StatusTransferenciaID = statusResposta.StatusTransferenciaID;
            solicitacao.UsuarioSolicitadoID = usuarioId;
            solicitacao.DataResposta = DateTime.UtcNow;

            _repository.Atualizar(solicitacao);

            if (dto.Aprovado)
            {
                StatusPatrimonio statusTransferido = _repository.BuscarStatusPatrimonioPorNome("Transferido");

                if (statusTransferido == null)
                {
                    throw new DomainException("Status de patrimônio 'transferido' não encontrado.");
                }

                TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Transferência");

                if (tipoAlteracao == null)
                {
                    throw new DomainException("Tipo de alteração 'Transferência' não encontrado.");
                }

                patrimonio.LocalizacaoID = solicitacao.LocalizacaoID;
                patrimonio.StatusPatrimonioID = statusTransferido.StatusPatrimonioID;

                _repository.AtualizarPatrimonio(patrimonio);

                LogPatrimonio log = new LogPatrimonio
                {
                    DataTransferencia = DateTime.UtcNow,
                    TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                    StatusPatrimonioID = statusTransferido.StatusPatrimonioID,
                    LocalizacaoID = solicitacao.LocalizacaoID,
                    PatrimonioID = solicitacao.PatrimonioID,
                    UsuarioID = usuarioId
                };

                _repository.AdicionarLog(log);
            }
        }
    }
}