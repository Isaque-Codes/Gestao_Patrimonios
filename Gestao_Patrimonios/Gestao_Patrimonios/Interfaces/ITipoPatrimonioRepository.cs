using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ITipoPatrimonioRepository
    {
        List<TipoPatrimonio> Listar();

        TipoPatrimonio BuscarPorId(Guid id);

        TipoPatrimonio BuscarPorNome(string nome);

        void Adicionar(TipoPatrimonio tipo);

        void Atualizar(TipoPatrimonio tipo);
    }
}
