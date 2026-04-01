using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();

        Endereco BuscarPorId(Guid enderecoId);

        Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid bairroId, Guid? enderecoId = null);

        bool BairroExistente(Guid bairroId);

        void Adicionar(Endereco endereco);

        void Atualizar(Endereco endereco);
    }
}
