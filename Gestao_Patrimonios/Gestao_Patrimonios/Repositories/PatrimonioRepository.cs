using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class PatrimonioRepository : IPatrimonioRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public PatrimonioRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Patrimonio> Listar()
        {
            return _context.Patrimonio.AsNoTracking()
                .OrderBy(p => p.Denominacao).ToList();
        }

        public Patrimonio BuscarPorId(Guid patrimonioId)
        {
            return _context.Patrimonio.AsNoTracking()
                .FirstOrDefault(p => p.PatrimonioID == patrimonioId);
        }
        public bool BuscarPorNumeroPatrimonio(string numeroPatrimonio)
        {
            return _context.Patrimonio.Any(patrimonio => patrimonio.NumeroPatrimonio == numeroPatrimonio);
        }

        public bool LocalizacaoExistente(Guid localizacaoId)
        {
            return _context.Localizacao.AsNoTracking()
                .Any(l => l.LocalizacaoID == localizacaoId);
        }

        public bool StatusPatrimonioExistente(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.AsNoTracking()
                .Any(s => s.StatusPatrimonioID == statusPatrimonioId);
        }

        public Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao)
        {
            return _context.Localizacao.AsNoTracking()
                .FirstOrDefault(l => l.NomeLocal.ToLower() == nomeLocalizacao.ToLower());
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

        public void Adicionar(Patrimonio patrimonio)
        {
            _context.Patrimonio.Add(patrimonio);
            _context.SaveChanges();
        }

        public void AtualizarStatus(Patrimonio patrimonio)
        {
            if (patrimonio == null)
            {
                return;
            }

            Patrimonio patrimonioBanco = _context.Patrimonio.Find(patrimonio.PatrimonioID);

            if (patrimonioBanco == null)
            {
                return;
            }

            patrimonioBanco.StatusPatrimonioID = patrimonio.StatusPatrimonioID;

            _context.SaveChanges();
        }

        public void AdicionarLog(LogPatrimonio logPatrimonio)
        {
            _context.LogPatrimonio.Add(logPatrimonio);
            _context.SaveChanges();
        }
    }
}