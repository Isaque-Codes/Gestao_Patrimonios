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
            return _context.Bairro
                .OrderBy(b => b.NomeBairro)
                .ToList();
        }

        public Bairro BuscarPorId(Guid bairroId)
        {
            return _context.Bairro.Find(bairroId);
        }

        public Bairro BuscarPorNome(string nomeBairro, Guid cidadeId)
        {
            return _context.Bairro.FirstOrDefault(b =>
                b.NomeBairro.ToLower() == nomeBairro.ToLower() &&
                b.CidadeID == cidadeId
            );
        }

        public bool CidadeExiste(Guid cidadeId)
        {
            return _context.Cidade.Any(c => c.CidadeID == cidadeId);
        }

        public void Adicionar(Bairro bairro)
        {
            _context.Bairro.Add(bairro);
            _context.SaveChanges();
        }

        public void Atualizar(Bairro bairro)
        {
            if (bairro == null)
            {
                return;
            }

            Bairro bairroBanco = _context.Bairro.Find(bairro.BairroID);

            if (bairroBanco == null)
            {
                return;
            }

            bairroBanco.NomeBairro = bairro.NomeBairro;
            bairroBanco.CidadeID = bairro.CidadeID;

            _context.SaveChanges();
        }
    }
}
