using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();

        Patrimonio BuscarPorId(Guid patrimonioId);

        // Usar AsQueryable
        Patrimonio BuscarPorNumeroPatrimonio(string numeroPatrimonio, Guid? patrimonioId = null);

        bool LocalizacaoExistente(Guid localizacaoId);

        bool TipoPatrimonioExistente(Guid tipoPatrimonioId);

        bool StatusPatrimonioExistente(Guid statusPatrimonioId);

        void Adicionar(Patrimonio patrimonio);

        void Atualizar(Patrimonio patrimonio);

        void AtualizarStatus(Patrimonio patrimonio);
    }
}
