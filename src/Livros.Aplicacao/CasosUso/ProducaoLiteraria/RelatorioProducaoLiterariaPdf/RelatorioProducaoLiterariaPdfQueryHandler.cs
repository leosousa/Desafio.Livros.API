
using Livros.Aplicacao.Base;
using Livros.Infraestrutura.Resolvers;
using MediatR;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using System.Runtime.InteropServices;

namespace Livros.Aplicacao.CasosUso.ProducaoLiteraria.RelatorioProducaoLiterariaPdf;

public class RelatorioProducaoLiterariaPdfQueryHandler : ServicoAplicacao,
    IRequestHandler<RelatorioProducaoLiterariaPdfQuery, RelatorioProducaoLiterariaPdfQueryResult>
{
    public async Task<RelatorioProducaoLiterariaPdfQueryResult> Handle(RelatorioProducaoLiterariaPdfQuery dadosRelatorio, CancellationToken cancellationToken)
    {
        RelatorioProducaoLiterariaPdfQueryResult relatorio = new();

        // Registrar o FontResolver
        string caminhoFonte = Path.Combine(Directory.GetCurrentDirectory(), "Recursos", "Fonts");
        GlobalFontSettings.FontResolver = new CustomFontResolver(caminhoFonte);

        // Criação do documento PDF
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Relatório de Prdução Literária";

        // Adiciona uma página
        PdfPage page = document.AddPage();

        // Objeto de desenho
        XGraphics gfx = XGraphics.FromPdfPage(page);

        Console.WriteLine("***********************************************************");
        Console.WriteLine("Directory.GetCurrentDirectory(): {0}", Directory.GetCurrentDirectory());
        Console.WriteLine("***********************************************************");

        Console.WriteLine("***********************************************************");
        Console.WriteLine("Caminho da fonte: {0}", caminhoFonte);
        Console.WriteLine("***********************************************************");

        var fontName = "DejaVu Sans";

        XFont titleFont = new XFont(fontName, 20);
        XFont headerFont = new XFont(fontName, 12);
        XFont bodyFont = new XFont(fontName, 8);

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

    private string GetSystemFontPath()
    {
        // Caminho para fontes no Linux
        var linuxFontDirectory = "/usr/share/fonts/truetype/dejavu/";
        var windowsFontDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);

        // Adicionar curinga para localizar a fonte
        string searchPattern = string.Empty;
        var searchPatternLinux = $"DejaVuSans.ttf";
        var searchPatternWindows = $"Arial.ttf";

        // Procurar no diretório de fontes do Windows
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            searchPattern = searchPatternWindows;
            var fontPath = Directory.GetFiles(windowsFontDirectory, searchPatternWindows).FirstOrDefault();
            if (!string.IsNullOrEmpty(fontPath))
            {
                return fontPath;
            }
        }

        // Procurar no diretório de fontes do Linux
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            searchPattern = searchPatternLinux;
            foreach (var subDir in Directory.GetDirectories(linuxFontDirectory))
            {
                var fontPath = Directory.GetFiles(subDir, searchPatternLinux).FirstOrDefault();
                if (!string.IsNullOrEmpty(fontPath))
                {
                    return fontPath;
                }
            }
        }

        throw new FileNotFoundException($"Fonte '{searchPattern}' não encontrada.");
    }
}