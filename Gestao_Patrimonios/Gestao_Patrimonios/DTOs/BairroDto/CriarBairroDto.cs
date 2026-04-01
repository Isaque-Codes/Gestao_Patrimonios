using Gestao_Patrimonios.Domains;

namespace Gestao_Patrimonios.DTOs.BairroDto
{
    public class CriarBairroDto
    {
        public string NomeBairro { get; set; } = string.Empty;

        public Guid CidadeID { get; set; }
    }
}
