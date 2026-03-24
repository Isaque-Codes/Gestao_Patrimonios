using System;
using System.Collections.Generic;

namespace Gestao_Patrimonios.Domains;

public partial class TipoPatrimonio
{
    public Guid TipoPatrimonioID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Patrimonio> Patrimonio { get; set; } = new List<Patrimonio>();
}
