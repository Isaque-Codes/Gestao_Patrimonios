using CsvHelper;
using CsvHelper.Configuration;
using Gestao_Patrimonios.Applications.Mapeamentos;
using Gestao_Patrimonios.Applications.Regras;
using Gestao_Patrimonios.Domains;
using Gestao_Patrimonios.DTOs.PatrimonioDto;
using Gestao_Patrimonios.Exceptions;
using Gestao_Patrimonios.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gestao_Patrimonios.Applications.Services
{
    public class PatrimonioService
    {
        private readonly IPatrimonioRepository _repository;

        public PatrimonioService(IPatrimonioRepository repository)
        {
            _repository = repository;
        }

        public List<ListarPatrimonioDto> Listar()
        {
            List<Patrimonio> patrimonios = _repository.Listar();

            List<ListarPatrimonioDto> dtos = patrimonios.Select(p => new ListarPatrimonioDto
            {
                PatrimonioID = p.PatrimonioID,
                Denominacao = p.Denominacao,
                NumeroPatrimonio = p.NumeroPatrimonio,
                Valor = p.Valor,
                Imagem = p.Imagem,
                LocalizacaoID = p.LocalizacaoID,
                StatusPatrimonioID = p.StatusPatrimonioID
            }).ToList();

            return dtos;
        }

        public ListarPatrimonioDto BuscarPorId(Guid patrimonioId)
        {
            Patrimonio patrimonio = _repository.BuscarPorId(patrimonioId);

            if (patrimonio == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            ListarPatrimonioDto dto = new ListarPatrimonioDto
            {
                PatrimonioID = patrimonio.PatrimonioID,
                Denominacao = patrimonio.Denominacao,
                NumeroPatrimonio = patrimonio.NumeroPatrimonio,
                Valor = patrimonio.Valor,
                Imagem = patrimonio.Imagem,
                LocalizacaoID = patrimonio.LocalizacaoID,
                StatusPatrimonioID = patrimonio.StatusPatrimonioID
            };

            return dto;
        }

        public void Adicionar(IFormFile arquivoCsv, Guid usuarioId)
        {
            if (arquivoCsv == null || arquivoCsv.Length == 0)
            {
                throw new DomainException("Arquivo CSV é obrigatório.");
            }

            Localizacao localizacaoSemLocal = _repository.BuscarLocalizacaoPorNome("Sem local");

            if (localizacaoSemLocal == null)
            {
                throw new DomainException("Localização 'Sem local' não cadastrada.");
            }

            StatusPatrimonio statusAtivo = _repository.BuscarStatusPatrimonioPorNome("Ativo");

            if (statusAtivo == null)
            {
                throw new DomainException("Status 'Ativo' não cadastrado.");
            }

            TipoAlteracao tipoAlteracao = _repository.BuscarTipoAlteracaoPorNome("Atualização de dados");

            if (tipoAlteracao == null)
            {
                throw new DomainException("Tipo de alteração 'Atualização de dados' não cadastrado.");
            }

            List<ImportarPatrimonioCsvDto> registros;

            // Abre o arquivo IFormFile enviado
            using (var stream = arquivoCsv.OpenReadStream())

            // Lê o arquivo como texto
            using (var reader = new StreamReader(stream))

            // Cria leitor do CSV com configurações personalizadas
            // CultureInfo: Define como números, datas e textos são interpretados.
            // InvariantCulture: padrão universal que garante consistência dos dados, independentemente da cultura do sistema.
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Define ponto e vírgula como separador
                Delimiter = ";",

                // Não trava a aplicação por conta da formatação
                // Ignora erros caso o cabeçalho seja divergente
                HeaderValidated = null,

                // Ignora o erro se faltar algum campo
                MissingFieldFound = null,

                // Ignora dados quebrados do CSV
                BadDataFound = null, // Exemplo: aspas abertas não fechadas

                // Remove espaços extras automaticamente
                TrimOptions = TrimOptions.Trim
            }))

            {
                // Registra o mapa
                csv.Context.RegisterClassMap<ImportarPatrimonioCsvMap>();

                // Converte para DTO
                registros = csv.GetRecords<ImportarPatrimonioCsvDto>().ToList();
            }

            var erros = new List<string>();

            foreach (var item in registros)
            {
                // Ignora registros sem número de patrimônio
                if (string.IsNullOrWhiteSpace(item.NumeroPatrimonio))
                {
                    continue;
                }

                // Ignora espaços extras do número
                string numeroPatrimonio = item.NumeroPatrimonio.Trim();

                // Ignora registros sem denominação
                if (string.IsNullOrWhiteSpace(item.Denominacao))
                {
                    erros.Add($"Patrimônio {numeroPatrimonio} ignorado: sem denominação.");
                    continue;
                }

                string denominacao = item.Denominacao.Trim();
                DateTime? dataIncorporacao = null;

                // Usa o formato brasileiro para leitura
                // E formata o DateTime
                if (!string.IsNullOrWhiteSpace(item.DataIncorporacao))
                {
                    if (DateTime.TryParse(item.DataIncorporacao, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime dataConvertida))
                    {
                        dataIncorporacao = dataConvertida;
                    }
                }

                decimal? valorAquisicao = null;

                if (!string.IsNullOrWhiteSpace(item.ValorAquisicao))
                {
                    // Remove separador de milhar e ajusta decimal
                    string valorTexto = item.ValorAquisicao
                        .Replace(".", "").Replace(",", ".");

                    // TryParse: Converte string em decimal
                    // NumberStyles.Any: Define formatos de número permitidos
                    // Any: Aceita qualquer número, seja com sinal, espaço ou etc
                    // out decimal valorConvertido: se der certo, cria a variável com o valor já convertido
                    if (decimal.TryParse(valorTexto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valorConvertido))
                    {
                        valorAquisicao = valorConvertido;
                    }

                    ValidarCampo.NumeroPatrimonio(numeroPatrimonio);
                    ValidarCampo.Denominação(denominacao);

                    bool patrimonioExistente = _repository.BuscarPorNumeroPatrimonio(numeroPatrimonio);

                    if (patrimonioExistente == true)
                    {
                        continue;
                    }

                    Patrimonio patrimonio = new Patrimonio
                    {
                        Denominacao = denominacao,
                        NumeroPatrimonio = numeroPatrimonio,
                        Valor = valorAquisicao,
                        Imagem = null,
                        LocalizacaoID = localizacaoSemLocal.LocalizacaoID,
                        StatusPatrimonioID = statusAtivo.StatusPatrimonioID
                    };

                    _repository.Adicionar(patrimonio);

                    LogPatrimonio log = new LogPatrimonio
                    {
                        DataTransferencia = dataIncorporacao ?? DateTime.Now,
                        TipoAlteracaoID = tipoAlteracao.TipoAlteracaoID,
                        StatusPatrimonioID = patrimonio.StatusPatrimonioID,
                        PatrimonioID = patrimonio.PatrimonioID,
                        UsuarioID = usuarioId,
                        LocalizacaoID = patrimonio.LocalizacaoID
                    };

                    _repository.AdicionarLog(log);
                }
            }
        }

        public void AtualizarStatus(Guid patrimonioId, AtualizarStatusPatrimonioDto dto)
        {
            Patrimonio patrimonioBanco = _repository.BuscarPorId(patrimonioId);

            if (patrimonioBanco == null)
            {
                throw new DomainException("Patrimônio não encontrado.");
            }

            if (!_repository.StatusPatrimonioExistente(dto.StatusPatrimonioID))
            {
                throw new DomainException("Status de patrimônio informado não existe.");
            }

            patrimonioBanco.StatusPatrimonioID = dto.StatusPatrimonioID;

            _repository.AtualizarStatus(patrimonioBanco);
        }
    }
}