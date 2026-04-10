using Gestao_Patrimonios.Exceptions;

namespace Gestao_Patrimonios.Applications.Regras
{
    public static class ValidarCampo
    {
        // O THIS possibilita o POLIMORFISMO DE EXTENSÃO
        private static void Validar(this string nome, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new DomainException(mensagem);
            }
        }

        // NOME GENÉRICO
        public static void Nome(this string nome) =>
            nome.Validar("O nome é obrigatório.");

        // ESTADO
        public static void Estado(this string estado) =>
            estado.Validar("O estado é obrigatório.");

        // CIDADE
        public static void Cidade(this string cidade) =>
            cidade.Validar("A cidade é obrigatória.");

        // BAIRRO
        public static void Bairro(this string bairro) =>
            bairro.Validar("O bairro é obrigatório.");

        // LOCALIZAÇÃO
        public static void Local(this string estado) =>
            estado.Validar("O local é obrigatório.");

        // ÁREA
        public static void Area(this string estado) =>
            estado.Validar("A área é obrigatória.");

        // ENDEREÇO
        public static void Logradouro(this string logradouro) =>
            logradouro.Validar("O logradouro é obrigatório.");

        // NIF
        public static void NIF(this string nif) =>
            nif.Validar("O NIF é obrigatório.");

        // CPF
        public static void CPF(this string cpf) =>
            cpf.Validar("O CPF é obrigatório.");

        // EMAIL
        public static void Email(this string email) =>
            email.Validar("O email é obrigatório.");

        // SENHA
        public static void Senha(this string senha) =>
            senha.Validar("O email é obrigatório.");

        // STATUS
        public static void Status(this string status) =>
            status.Validar("O status é obrigatório.");

        // CARGO
        public static void Cargo(this string cargo) =>
            cargo.Validar("O status é obrigatório.");

        // JUSTIFICATIVA
        public static void Justificativa(this string justificativa) =>
            justificativa.Validar("A Justificativa é obrigatória.");

        // NÚMERO DE PATRIMÔNIO
        public static void NumeroPatrimonio(this string numeroPatrimonio) =>
            numeroPatrimonio.Validar("O número do patrimônio é obrigatório.");

        // DENOMINAÇÃO
        public static void Denominação(this string denominação) =>
            denominação.Validar("A denominação é obrigatória.");
    }
}