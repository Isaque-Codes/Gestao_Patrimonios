using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class SolicitacaoTransferenciaRepository : ISolicitacaoTransferenciaRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public SolicitacaoTransferenciaRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<SolicitacaoTransferencia> Listar()
        {
            return _context.SolicitacaoTransferencia.AsNoTracking()
                .OrderByDescending(s => s.DataCriacaoSolicitante)
                .ToList();
        }

        public SolicitacaoTransferencia BuscarPorId(Guid transferenciaId)
        {
            return _context.SolicitacaoTransferencia.AsNoTracking()
                .FirstOrDefault(s => s.TransferenciaID == transferenciaId);
        }

        public StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus)
        {
            return _context.StatusTransferencia.AsNoTracking()
                .FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public bool SolicitacaoPendenteExistente(Guid patrimonioId)
        {
            StatusTransferencia statusPendente = BuscarStatusTransferenciaPorNome("Pendente de aprovação");

            if (statusPendente == null)
            {
                return false;
            }

            return _context.SolicitacaoTransferencia.AsNoTracking().Any(s =>
            s.PatrimonioID == patrimonioId &&
            s.StatusTransferenciaID == statusPendente.StatusTransferenciaID);
        }

        public bool ResponsavelPeloLocal(Guid usuarioId, Guid localizacaoId)
        {
            return _context.Usuario.AsNoTracking().Any(u =>
                u.UsuarioID == usuarioId &&
                u.Localizacao.Any(l => l.LocalizacaoID == localizacaoId));
        }

        public bool LocalizacaoExistente(Guid localizacaoId)
        {
            return _context.Localizacao.AsNoTracking()
                .Any(l => l.LocalizacaoID == localizacaoId);
        }

        public Patrimonio BuscarPatrimonioPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.AsNoTracking()
                .FirstOrDefault(p => p.PatrimonioID == patrimonioId);
        }

        public void Adicionar(SolicitacaoTransferencia solicitacao)
        {
            _context.SolicitacaoTransferencia.Add(solicitacao);
            _context.SaveChanges();
        }

        public StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.AsNoTracking()
                .FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo)
        {
            return _context.TipoAlteracao.AsNoTracking()
                .FirstOrDefault(t => t.NomeTipo.ToLower() == nomeTipo.ToLower());
        }

        public void Atualizar(SolicitacaoTransferencia solicitacao)
        {
            if (solicitacao == null)
            {
                return;
            }

            SolicitacaoTransferencia solicitacaoBanco = _context.SolicitacaoTransferencia
                .Find(solicitacao.TransferenciaID);

            if (solicitacaoBanco == null)
            {
                return;
            }

            solicitacaoBanco.DataResposta = solicitacao.DataResposta;
            solicitacaoBanco.StatusTransferenciaID = solicitacao.StatusTransferenciaID;
            solicitacaoBanco.UsuarioSolicitadoID = solicitacao.UsuarioSolicitadoID;

            _context.SaveChanges();
        }

        public void AtualizarPatrimonio(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio
                .Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.LocalizacaoID = patrimonio.LocalizacaoID;
            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio log)
        {
            _context.LogPatrimonio.Add(log);
            _context.SaveChanges();
        }
    }
}