using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.Interfaces
{
    public interface IBairroRepository
    {
        List<Bairro> Listar();

        Bairro BuscarPorId(Guid bairroId);

        Bairro BuscarPorNome(string nomeBairro, Guid cidadeId);

        bool CidadeExistente(Guid cidadeId);

        void Adicionar(Bairro bairro);

        void Atualizar(Bairro bairro);
    }
}
