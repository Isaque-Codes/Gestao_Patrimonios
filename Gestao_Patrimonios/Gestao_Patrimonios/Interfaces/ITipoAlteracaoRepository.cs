using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ITipoAlteracaoRepository
    {
        List<TipoAlteracao> Listar();

        TipoAlteracao BuscarPorId(Guid tipoAlteracaoId);

        TipoAlteracao BuscarPorNome(string nomeTipo);

        void Adicionar(TipoAlteracao tipoAlteracao);

        void Atualizar(TipoAlteracao tipoAlteracao);
    }
}
