using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ILogPatrimonioRepository
    {
        List<LogPatrimonio> Listar();

        List<LogPatrimonio> BuscarPorPatrimonio(Guid patrimonioId);
    }
}
