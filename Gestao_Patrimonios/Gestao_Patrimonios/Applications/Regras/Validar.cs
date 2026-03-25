using Gestao_Patrimonios.Exceptions;

namespace Gestao_Patrimonios.Applications.Regras
{
    public class Validar
    {
        public static void validarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException("O nome é obrigatório.");
            }
        }
    }
}
