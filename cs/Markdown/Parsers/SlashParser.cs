using Markdown.TagsType;

namespace Markdown.Parsers;

public class SlashParser
{

    private int Index;

    public SlashParser()
    {
        Index = 0;
    }
    public Token QuotedToken(string text, int i, bool isNextSymbolBold)
    {
        i++;
        Index = i;
        if (Index < text.Length)
        {
            switch (text[Index])
            {
                case '_':
                    if (Index + 1 < text.Length && isNextSymbolBold)
                    {
                        Index++;
                        return new Token(new BoldTag().MarkdownTag);
                    }
                    return new Token(new ItalicTag().MarkdownTag);
                case '#':
                    return new Token(new HeadingTag().MarkdownTag);
                case '\\':
                    return new Token("\\");
            }
        }

        Index--;
        return new Token("\\");
    }
    
    public int GetIndex() => Index;
    
}