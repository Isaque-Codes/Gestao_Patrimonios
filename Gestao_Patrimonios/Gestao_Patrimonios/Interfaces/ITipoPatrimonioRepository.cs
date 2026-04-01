using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ITipoPatrimonioRepository
    {
        List<TipoPatrimonio> Listar();

        TipoPatrimonio BuscarPorId(Guid tipoPatrimonioId);

        TipoPatrimonio BuscarPorNome(string nomeTipo);

        void Adicionar(TipoPatrimonio tipoPatrimonio);

        void Atualizar(TipoPatrimonio tipoPatrimonio);
    }
}
