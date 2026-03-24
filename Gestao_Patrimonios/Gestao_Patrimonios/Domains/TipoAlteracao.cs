using System;
using System.Collections.Generic;

namespace Gestao_Patrimonios.Domains;

public partial class TipoAlteracao
{
    public Guid TipoAlteracaoID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();
}
