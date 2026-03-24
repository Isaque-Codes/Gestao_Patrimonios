using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public AreaRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Area> Listar()
        {
            return _context.Area.OrderBy(a => a.NomeArea).ToList();
        }
    }
}
