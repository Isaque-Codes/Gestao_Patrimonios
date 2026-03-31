using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public UsuarioRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(u => u.Nome).ToList();
        }
    }
}
