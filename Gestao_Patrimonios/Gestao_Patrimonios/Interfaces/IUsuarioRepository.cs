using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> Listar();

        Usuario BuscarPorId(Guid usuarioId);

        Usuario BuscarPorNIFComTipoUsuario(string nif);

        Usuario BuscarDuplicata(string nif, string cpf, string email, Guid? usuarioId = null);

        bool EnderecoExistente(Guid enderecoId);

        bool CargoExistente(Guid cargoId);

        bool TipoUsuarioExistente(Guid tipoUsuarioId);

        void Adicionar(Usuario usuario);

        void Atualizar(Usuario usuario);

        void AtualizarSenha(Usuario usuario);

        void AtualizarStatus(Usuario usuario);

        void AtualizarPrimeiroAcesso(Usuario usuario);
    }
}
