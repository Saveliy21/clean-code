using Markdown.TagsType;

namespace Markdown.Parsers;

public class ItalicParser
{
    private readonly GeneralParser.IsValidTag isValidOpenItalicTag;
    private readonly GeneralParser.IsValidTag isValidCloseItalicTag;
    private readonly GeneralParser.AddInsert addItalicTag;
    private bool isItalicClosed;
    private readonly Stack<Token> tagsStack;
    public bool IsHaveInsideItalicTag;


    public ItalicParser()
    {
        isItalicClosed = true;
        tagsStack = GeneralParser.tagsStack;
        addItalicTag = GeneralParser.AddInsideTag;
        isValidOpenItalicTag = GeneralParser.IsValidOpenTag;
        isValidCloseItalicTag = GeneralParser.IsValidCloserTag;
        IsHaveInsideItalicTag = false;
    }

    public Token ItalicParse(string text, int i)
    {
        if (i != 0 && i != text.Length - 1)
            IsHaveInsideItalicTag = true;
        if (isItalicClosed)
        {
            if (!isValidOpenItalicTag(text, i))
            {
                return new Token("_");
            }
        }
        else
        {
            if (!isValidCloseItalicTag(text, i))
            {
                return new Token("_");
            }
        }
        
        if (tagsStack.Count > 1 && !isItalicClosed && BoldParser.IsBoldClosed)
        {
            tagsStack.Pop();
        }

        var result = addItalicTag(new ItalicTag(), isItalicClosed);
        isItalicClosed = !isItalicClosed;
        if (!isItalicClosed && i == text.Length - 1)
        {
            tagsStack.Pop();
        }

        return result;
    }
}