using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _context.Area.AsNoTracking()
                .OrderBy(a => a.NomeArea)
                .ToList();
        }

        public Area BuscarPorId(Guid areaId)
        {
            return _context.Area.AsNoTracking()
                .FirstOrDefault(a => a.AreaID == areaId);
        }

        public Area BuscarPorNome(string nomeArea)
        {
            return _context.Area.AsNoTracking()
                .FirstOrDefault(a => a.NomeArea.ToLower() == nomeArea.ToLower());
        }

        public void Adicionar(Area area)
        {
            _context.Area.Add(area);

            _context.SaveChanges();
        }

        public void Atualizar(Area area)
        {
            if (area == null)
            {
                return;
            }

            Area areaBanco = _context.Area.Find(area.AreaID);

            areaBanco.NomeArea = area.NomeArea;

            _context.SaveChanges();
        }
    }
}