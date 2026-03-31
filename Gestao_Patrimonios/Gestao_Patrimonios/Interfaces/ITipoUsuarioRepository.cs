using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();

        TipoUsuario BuscarPorId(Guid id);

        TipoUsuario BuscarPorNome(string nome);

        void Adicionar(TipoUsuario tipo);

        void Atualizar(TipoUsuario tipo);
    }
}
