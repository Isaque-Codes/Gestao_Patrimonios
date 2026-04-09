using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface ISolicitacaoTransferenciaRepository
    {
        List<SolicitacaoTransferencia> Listar();

        SolicitacaoTransferencia BuscarPorId(Guid transferenciaId);

        bool SolicitacaoPendenteExistente(Guid patrimonioId);

        bool ResponsavelPeloLocal(Guid usuarioId, Guid localizacaoId);

        bool LocalizacaoExistente(Guid localizacaoId);

        Patrimonio BuscarPatrimonioPorId(Guid patrimonioId);

        StatusTransferencia BuscarStatusTransferenciaPorNome(string nomeStatus);

        StatusPatrimonio BuscarStatusPatrimonioPorNome(string nomeStatus);

        TipoAlteracao BuscarTipoAlteracaoPorNome(string nomeTipo);

        void Adicionar(SolicitacaoTransferencia solicitacao);

        void Atualizar(SolicitacaoTransferencia solicitacao);

        void AtualizarPatrimonio(Patrimonio patrimonio);

        void AdicionarLog(LogPatrimonio log);
    }
}