namespace Markdown;

public class Md
{
    public static string Render(string markdown)
    {
        Preparator preparator = new Preparator();
        Converter converter = new Converter();
        return converter.ConvertWithTokens(preparator.GetTokens(markdown));
    }
}