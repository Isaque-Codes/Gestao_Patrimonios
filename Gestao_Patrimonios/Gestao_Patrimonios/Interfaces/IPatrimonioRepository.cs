using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IPatrimonioRepository
    {
        List<Patrimonio> Listar();

        Patrimonio BuscarPorId(Guid patrimonioId);

        bool BuscarPorNumeroPatrimonio(string numeroPatrimonio);

        bool LocalizacaoExistente(Guid localizacaoId);

        bool StatusPatrimonioExistente(Guid statusPatrimonioId);

        void Adicionar(Patrimonio patrimonio);

        void AtualizarStatus(Patrimonio patrimonio);

        void AdicionarLog(LogPatrimonio logPatrimonio);

        Localizacao BuscarLocalizacaoPorNome(string nomeLocalizacao);

        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);

        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);
    }
}
