using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IStatusTransferenciaRepository
    {
        List<StatusTransferencia> Listar();

        StatusTransferencia BuscarPorId(Guid statusTransferenciaId);

        StatusTransferencia BuscarPorNome(string nomeStatus);

        void Adicionar(StatusTransferencia statusTransferencia);

        void Atualizar(StatusTransferencia statusTransferencia);
    }
}
