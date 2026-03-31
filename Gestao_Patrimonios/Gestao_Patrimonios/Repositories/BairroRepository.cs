using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Gestao_Patrimonios.Repositories
{
    public class BairroRepository : IBairroRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public BairroRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Bairro> Listar()
        {
            return _context.Bairro.OrderBy(b => b.NomeBairro).ToList();
        }

        public Bairro BuscarPorId(Guid id)
        {
            return _context.Bairro.FirstOrDefault(b => b.BairroID == id);
        }

        public Bairro BuscarPorNome(string nome)
        {
            return _context.Bairro.FirstOrDefault(b => b.NomeBairro == nome);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);

            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairroBanco == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.Cidade = bairro.Cidade;

            _context.SaveChanges();
        }
    }
}
