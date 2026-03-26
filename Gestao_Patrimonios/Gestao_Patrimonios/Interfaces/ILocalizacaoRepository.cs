using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();

        Localizacao BuscarPorId(Guid id);

        bool AreaExistente(Guid areaId);

        void Adicionar(Localizacao localizacao);

        void Atualizar(Localizacao localizacao);
    }
}
