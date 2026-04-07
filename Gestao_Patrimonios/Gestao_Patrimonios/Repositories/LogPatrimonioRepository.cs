using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class LogPatrimonioRepository : ILogPatrimonioRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public LogPatrimonioRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<LogPatrimonio> Listar()
        {
            return _context.LogPatrimonio.AsNoTracking()
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }

        public List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId)
        {
            return _context.LogPatrimonio.AsNoTracking()
                .Where(log => log.PatrimonioID == patrimonioId)
                .Include(log => log.Usuario)
                .Include(log => log.Localizacao)
                .Include(log => log.TipoAlteracao)
                .Include(log => log.StatusPatrimonio)
                .Include(log => log.Patrimonio)
                .OrderByDescending(log => log.DataTransferencia)
                .ToList();
        }
    }
}