using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gestao_Patrimonios.Repositories
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public CidadeRepository(Gestao_PatrimoniosContext context)
        {
            _context = context;
        }

        public List<Cidade> Listar()
        {
            return _context.Cidade.OrderBy(c => c.NomeCidade).ToList();
        }

        public Cidade BuscarPorId(Guid cidadeId)
        {
            return _context.Cidade.Find(cidadeId);
        }

        public Cidade? BuscarPorNomeEEstado(string nomeCidade, string estado)
        {
            return _context.Cidade.FirstOrDefault(cidade =>
                cidade.NomeCidade.ToLower() == nomeCidade.ToLower() &&
                cidade.Estado.ToLower() == estado.ToLower());
        }

        public void Adicionar(Cidade cidade)
        {
            _context.Add(cidade);

            _context.SaveChanges();
        }

        public void Atualizar(Cidade cidade)
        {
            Cidade cidadeBanco = _context.Cidade.Find(cidade.CidadeID);

            if (cidadeBanco == null)
            {
                return;
            }

            cidadeBanco.NomeCidade = cidade.NomeCidade;
            cidadeBanco.Estado = cidade.Estado;

            _context.SaveChanges();
        }
    }
}