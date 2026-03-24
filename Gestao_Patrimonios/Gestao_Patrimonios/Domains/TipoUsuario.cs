using System;
using System.Collections.Generic;

namespace Gestao_Patrimonios.Domains;

public partial class TipoUsuario
{
    public Guid TipoUsuarioID { get; set; }

    public string NomeTipo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
