using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();
    }
}
