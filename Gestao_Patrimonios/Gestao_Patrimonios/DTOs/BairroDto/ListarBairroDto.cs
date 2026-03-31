using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.DTOs.BairroDto
{
    public class ListarBairroDto
    {
        public Guid BairroID { get; set; }

        public string NomeBairro { get; set; } = string.Empty;

        public Cidade Cidade { get; set; }
    }
}
