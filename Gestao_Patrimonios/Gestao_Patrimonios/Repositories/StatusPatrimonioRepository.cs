using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class StatusPatrimonioRepository : IStatusPatrimonioRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public StatusPatrimonioRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<StatusPatrimonio> Listar()
        {
            return _context.StatusPatrimonio.AsNoTracking()
                .OrderBy(status => status.NomeStatus)
                .ToList();
        }

        public StatusPatrimonio BuscarPorId(Guid statusPatrimonioId)
        {
            return _context.StatusPatrimonio.AsNoTracking()
                .FirstOrDefault(s => s.StatusPatrimonioID == statusPatrimonioId);
        }

        public StatusPatrimonio BuscarPorNome(string nomeStatus)
        {
            return _context.StatusPatrimonio.AsNoTracking()
                .FirstOrDefault(s => s.NomeStatus.ToLower() == nomeStatus.ToLower());
        }

        public void Adicionar(StatusPatrimonio statusPatrimonio)
        {
            _context.StatusPatrimonio.Add(statusPatrimonio);
            _context.SaveChanges();
        }

        public void Atualizar(StatusPatrimonio statusPatrimonio)
        {
            if (statusPatrimonio == null)
            {
                return;
            }

            StatusPatrimonio statusBanco = _context.StatusPatrimonio.Find(statusPatrimonio.StatusPatrimonioID);

            if (statusBanco == null)
            {
                return;
            }

            statusBanco.NomeStatus = statusPatrimonio.NomeStatus;

            _context.SaveChanges();
        }
    }
}
