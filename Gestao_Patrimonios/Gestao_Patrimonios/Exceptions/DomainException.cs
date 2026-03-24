namespace Gestao_Patrimonios.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string mensagem) : base(mensagem) { }
    }
}
