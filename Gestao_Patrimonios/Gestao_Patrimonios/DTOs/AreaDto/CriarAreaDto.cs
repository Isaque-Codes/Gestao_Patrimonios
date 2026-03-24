using System.ComponentModel.DataAnnotations;

namespace Gestao_Patrimonios.DTOs.AreaDto
{
    public class CriarAreaDto
    {
        [Required(ErrorMessage = "O nome da área é obrigatório.")]
        [StringLength(50, ErrorMessage = "O limite de caracteres da área é 50.")]
        public string NomeArea { get; set; } = string.Empty;
    }
}
