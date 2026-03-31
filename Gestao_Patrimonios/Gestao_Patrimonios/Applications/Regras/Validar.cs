using Gestao_Patrimonios.Exceptions;

namespace Gestao_Patrimonios.Applications.Regras
{
    public static class Validar
    {
        // O THIS atua na fluídez da extensão (POLIMORFISMO DE EXTENSÃO)
        private static void ValidarNome(this string nome, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarNome(this string nome) =>
            nome.ValidarNome("O nome é obrigatório.");

        public static void ValidarEstado(this string estado) =>
            estado.ValidarNome("O estado é obrigatório.");

        public static void ValidarCidade(this string cidade) =>
            cidade.ValidarNome("A cidade é obrigatória.");

        public static void ValidarBairro(this string bairro) =>
            bairro.ValidarNome("O bairro é obrigatório.");

        public static void ValidarLocal(this string estado) =>
            estado.ValidarNome("O local é obrigatório.");

        public static void ValidarArea(this string estado) =>
            estado.ValidarNome("A área é obrigatória.");
    }
}