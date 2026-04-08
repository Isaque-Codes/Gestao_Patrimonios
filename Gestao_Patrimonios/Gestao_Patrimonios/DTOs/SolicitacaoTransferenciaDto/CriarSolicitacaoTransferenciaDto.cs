namespace Gestao_Patrimonios.DTOs.SolicitacaoTransferenciaDto
{
    public class CriarSolicitacaoTransferenciaDto
    {
        public Guid PatrimonioID { get; set; }

        public Guid LocalizacaoID { get; set; }

        public string Justificativa { get; set; } = string.Empty;
    }
}
