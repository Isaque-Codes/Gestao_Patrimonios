using CsvHelper.Configuration;
using Gestao_Patrimonios.DTOs.PatrimonioDto;

namespace Gestao_Patrimonios.Applications.Mapeamentos
{
    // ClassMap: Define como ler o CSV, um "TRADUTOR DE COLUNAS"
    public class ImportarPatrimonioCsvMap : ClassMap<ImportarPatrimonioCsvDto>
    {
        // Definição dos mapeamentos
        public ImportarPatrimonioCsvMap()
        {
            // MAP: Escolhe a propriedade do DTO
            // NAME: Define o nome da coluna CSV para a propriedade
            Map(m => m.NumeroPatrimonio).Name("N° invent.");
            Map(m => m.Denominacao).Name("Denominação do imobilizado");
            Map(m => m.DataIncorporacao).Name("Dt. incorp.");
            Map(m => m.ValorAquisicao).Name("ValAquis.");
        }
    }
}