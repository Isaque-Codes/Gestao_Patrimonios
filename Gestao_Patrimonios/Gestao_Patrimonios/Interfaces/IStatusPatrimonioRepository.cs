using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IStatusPatrimonioRepository
    {
        List<StatusPatrimonio> Listar();

        StatusPatrimonio BuscarPorId(Guid statusPatrimonioId);

        StatusPatrimonio BuscarPorNome(string nomeStatus);

        void Adicionar(StatusPatrimonio statusPatrimonio);

        void Atualizar(StatusPatrimonio statusPatrimonio);
    }
}
