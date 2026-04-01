using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();

        TipoUsuario BuscarPorId(Guid tipoUsuarioId);

        TipoUsuario BuscarPorNome(string nomeTipo);

        void Adicionar(TipoUsuario tipoUsuario);

        void Atualizar(TipoUsuario tipoUsuario);
    }
}
