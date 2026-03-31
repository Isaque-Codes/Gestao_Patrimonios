using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();

        Bairro BuscarPorId(Guid id);

        Bairro BuscarPorNome(string nome);

        void Adicionar(Bairro bairro);

        void Atualizar(Bairro bairro);

    }
}
