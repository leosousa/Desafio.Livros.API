

using PdfSharpCore.Fonts;

namespace Livros.Infraestrutura.Resolvers;

public class CustomFontResolver : IFontResolver
{
    private readonly string _fontPath;

    public CustomFontResolver(string fontPath)
    {
        _fontPath = fontPath;
    }

    public string DefaultFontName => "DejaVu Sans";

    public byte[] GetFont(string faceName)
    {
        // Mapear o nome da fonte para o caminho do arquivo .ttf
        if (faceName == DefaultFontName)
        {
            var fontFilePath = Path.Combine(_fontPath, "DejaVuSans.ttf");
            if (File.Exists(fontFilePath))
            {
                return File.ReadAllBytes(fontFilePath);
            }
        }

        throw new FileNotFoundException($"Font {faceName} not found.");
    }

    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        // Mapeamento simples para fontes locais
        if (familyName == DefaultFontName)
        {
            return new FontResolverInfo(DefaultFontName);
        }

        return null;
    }
}