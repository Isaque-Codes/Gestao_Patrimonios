namespace Gestao_Patrimonios.DTOs.SolicitacaoTransferenciaDto
{
    public class ListarSolicitacaoTransferenciaDto
    {
        public Guid TransferenciaID { get; set; }

        public Guid PatrimonioID { get; set; }

        public Guid LocalizacaoID { get; set; }

        public string Justificativa { get; set; } = string.Empty;

        public Guid StatusTransferenciaId { get; set; }

        public Guid UsuarioSolicitanteID { get; set; }

        public DateTime DataCriacaoSolicitante { get; set; }

        public Guid? UsuarioSolicitadoID { get; set; }

        public DateTime? DataResposta { get; set; }
    }
}