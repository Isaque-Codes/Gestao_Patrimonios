using Gestao_Patrimonios.Exceptions;

namespace Gestao_Patrimonios.Applications.Regras
{
    public static class ValidarNome
    {
        // O THIS possibilita o POLIMORFISMO DE EXTENSÃO
        private static void Validar(this string nome, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void Validar(this string nome) =>
            nome.Validar("O nome é obrigatório.");

        public static void Estado(this string estado) =>
            estado.Validar("O estado é obrigatório.");

        public static void Cidade(this string cidade) =>
            cidade.Validar("A cidade é obrigatória.");

        public static void Bairro(this string bairro) =>
            bairro.Validar("O bairro é obrigatório.");

        public static void Local(this string estado) =>
            estado.Validar("O local é obrigatório.");

        public static void Area(this string estado) =>
            estado.Validar("A área é obrigatória.");
    }
}