using System; 
using System.Collections.Generic; 
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Previewer;
using QuestPDF.Helpers;
using AgendamentoBackend.Controllers;
using QuestPDF.Infrastructure;
using BackEndPlanejadorDeViagem.Controllers;

public class PDFGenerator
{
    private readonly AgendamentoController _agenda;

    public PDFGenerator(AgendamentoController agenda)
    {
        _agenda = agenda;
    }

    public List<string> ListNomesEExame()
    {
        var listaNomesEExame = new List<string>();

        foreach (var item in _agenda.NomeExame)
        {
            string nomeExameFormatado = $"{item.Key}: {item.Value}";
            listaNomesEExame.Add(nomeExameFormatado);
        }

        return listaNomesEExame;
    }

    public async Task<byte[]> GeneratePdfBytes()
    {
        DateTime dataDeHoje = DateTime.Today;
        string dataFormatada = dataDeHoje.ToString("dd/MM/yyyy");

        using (MemoryStream memoryStream = new MemoryStream())
        {


            QuestPDF.Settings.License = LicenseType.Community;

            var nomesEExames = ListNomesEExame();

            Document.Create(conteudo =>
            {
                conteudo.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(value: 2, Unit.Centimetre);
                    page.PageColor(Colors.White);

                    page.Header()
                        .Text($"Lista De Exames do dia {dataFormatada}")
                        .SemiBold()
                        .FontSize(35);

                    page.Content()
                        .PaddingVertical(value: 1, Unit.Centimetre)
                        .Column(x =>
                        {
                            foreach (var item in nomesEExames)
                            {
                                x.Item()
                                .Text(item);
                            }
                        });
                    page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
                });
            }).GeneratePdf(memoryStream);

            return memoryStream.ToArray();
        }
    }
}




