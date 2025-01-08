
using Livros.Aplicacao.Base;
using MediatR;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiterariaPdf;

public class RelatorioProducaoLiterariaPdfQueryHandler : ServicoAplicacao,
    IRequestHandler<RelatorioProducaoLiterariaPdfQuery, RelatorioProducaoLiterariaPdfQueryResult>
{
    public async Task<RelatorioProducaoLiterariaPdfQueryResult> Handle(RelatorioProducaoLiterariaPdfQuery dadosRelatorio, CancellationToken cancellationToken)
    {
        RelatorioProducaoLiterariaPdfQueryResult relatorio = new();

        // Criação do documento PDF
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Relatório de Prdução Literária";

        // Adiciona uma página
        PdfPage page = document.AddPage();

        // Objeto de desenho
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Definir fonte
        XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);
        XFont headerFont = new XFont("Arial", 12, XFontStyle.Bold);
        XFont bodyFont = new XFont("Arial", 8, XFontStyle.Regular);

        // Adiciona título
        gfx.DrawString("Relatório de Produção Literária", titleFont, XBrushes.Black, new XPoint(50, 50));

        // Coordenadas iniciais da tabela
        double x = 50, y = 100;
        double cellWidth = 130;
        double cellHeight = 20;
        double cellPublishYear = 40;

        // Cabeçalho da tabela
        gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x, y, cellWidth, cellHeight);
        gfx.DrawString("Autor", headerFont, XBrushes.Black, new XPoint(x + 5, y + 15));

        gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x + cellWidth, y, cellWidth, cellHeight);
        gfx.DrawString("Título", headerFont, XBrushes.Black, new XPoint(x + cellWidth + 5, y + 15));

        gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x + 2 * cellWidth, y, cellPublishYear, cellHeight);
        gfx.DrawString("Ano", headerFont, XBrushes.Black, new XPoint(x + 2 * cellWidth + 5, y + 15));

        gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x + 2 * cellWidth + cellPublishYear, y, 200, cellHeight);
        gfx.DrawString("Assuntos", headerFont, XBrushes.Black, new XPoint(x + 2 * cellWidth + cellPublishYear + 5, y + 15));

        y += cellHeight;

        // Preenche a tabela com os dados
        foreach (var producaoLiteraria in dadosRelatorio.Dados.Itens)
        {
            gfx.DrawRectangle(XPens.Black, XBrushes.White, x, y, cellWidth, cellHeight);
            gfx.DrawString(producaoLiteraria.Autor.Nome, bodyFont, XBrushes.Black, new XPoint(x + 5, y + 15));

            gfx.DrawRectangle(XPens.Black, XBrushes.White, x + cellWidth, y, cellWidth, cellHeight);
            gfx.DrawString(producaoLiteraria.Livro.Titulo, bodyFont, XBrushes.Black, new XPoint(x + cellWidth + 5, y + 15));
            
            gfx.DrawRectangle(XPens.Black, XBrushes.White, x + 2 * cellWidth, y, cellPublishYear, cellHeight);
            gfx.DrawString(producaoLiteraria.Livro.AnoPublicacao.ToString(), bodyFont, XBrushes.Black, new XPoint(x + 2 * cellWidth + 5, y + 15));

            gfx.DrawRectangle(XPens.Black, XBrushes.White, x + 2 * cellWidth + cellPublishYear, y, 200, cellHeight);
            gfx.DrawString(string.Join(',', producaoLiteraria.Assuntos), bodyFont, XBrushes.Black, new XPoint(x + 2 * cellWidth + cellPublishYear + 5, y + 15));

            y += cellHeight;
        }

        using (var memoryStream = new MemoryStream())
        {
            // Salva o documento no stream
            document.Save(memoryStream);

            // Retorna os bytes do stream
            relatorio.PdfFile = memoryStream.ToArray();
        }

        return await Task.FromResult(relatorio);
    }
}