using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();

        Localizacao BuscarPorId(Guid id);

        Localizacao BuscarPorNome(Guid areaId, string nomeLocal);

        bool AreaExistente(Guid areaId);

        void Adicionar(Localizacao localizacao);

        void Atualizar(Localizacao localizacao);
    }
}
