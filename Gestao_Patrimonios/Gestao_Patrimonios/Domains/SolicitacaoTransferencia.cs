using System;
using System.Collections.Generic;

namespace Gestao_Patrimonios.Domains;

public partial class SolicitacaoTransferencia
{
    public Guid TransferenciaID { get; set; }

    public DateTime DataCriacaoSolicitante { get; set; }

    public DateTime? DataResposta { get; set; }

    public string Justificativa { get; set; } = null!;

    public Guid StatusTransferenciaID { get; set; }

    public Guid UsuarioSolicitanteID { get; set; }

    public Guid? UsuarioSolicitadoID { get; set; }

    public Guid PatrimonioID { get; set; }

    public Guid LocalizacaoID { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual Patrimonio Patrimonio { get; set; } = null!;

    public virtual StatusTransferencia StatusTransferencia { get; set; } = null!;

    public virtual Usuario? UsuarioSolicitado { get; set; }

    public virtual Usuario UsuarioSolicitante { get; set; } = null!;
}
