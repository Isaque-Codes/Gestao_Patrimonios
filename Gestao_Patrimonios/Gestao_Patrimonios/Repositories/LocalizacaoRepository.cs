using Gestao_Patrimonios.Contexts;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.Interfaces;

namespace Gestao_Patrimonios.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly Gestao_PatrimoniosContext _context;

        public LocalizacaoRepository(Gestao_PatrimoniosContext context)

        {
            _context = context;
        }

        public List<Localizacao> Listar()
        {
            return _context.Localizacao.OrderBy(l => l.NomeLocal).ToList();
        }

        public Localizacao BuscarPorId(Guid localizacaoId)
        {
            return _context.Localizacao.Find(localizacaoId);
        }

        public Localizacao BuscarPorNome(Guid areaId, string nomeLocal)
        {
            return _context.Localizacao.FirstOrDefault
                (l => l.NomeLocal.ToLower() == nomeLocal.ToLower() && l.AreaID == areaId);
        }

        public bool AreaExistente(Guid areaId)
        {
            return _context.Area.Any(area => area.AreaID == areaId);
        }

        public void Adicionar(Localizacao localizacao)
        {
            _context.Localizacao.Add(localizacao);

            _context.SaveChanges();
        }

        public void Atualizar(Localizacao localizacao)
        {
            if (localizacao == null)
            {
                return;
            }

            Localizacao localizacaoBanco = _context.Localizacao.Find(localizacao.LocalizacaoID);

            if (localizacaoBanco == null)
            {
                return;
            }

            localizacaoBanco.NomeLocal = localizacao.NomeLocal;
            localizacaoBanco.LocalSAP = localizacao.LocalSAP;
            localizacaoBanco.DescricaoSAP = localizacao.DescricaoSAP;
            localizacaoBanco.AreaID = localizacao.AreaID;

            _context.SaveChanges();
        }
    }
}
