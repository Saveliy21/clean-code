using System.Collections;
using Markdown.TagsType;

namespace Markdown.Parsers;

public class BoldParser
{
    private readonly Stack<Token> tagsStack;
    private readonly GeneralParser.IsValidTag isValidOpenBoldTag;
    private readonly GeneralParser.IsValidTag isValidCloseBoldTag;
    private readonly GeneralParser.AddInsert addBoldTag;
    public bool IsHaveInsideBoldTag;
    public static bool IsBoldClosed;
    private int index;

    public BoldParser()
    {
        IsBoldClosed = true;
        IsHaveInsideBoldTag = false;
        tagsStack = GeneralParser.tagsStack;
        addBoldTag = GeneralParser.AddInsideTag;
        isValidOpenBoldTag = GeneralParser.IsValidOpenTag;
        isValidCloseBoldTag = GeneralParser.IsValidCloserTag;
        index = 0;
    }

    public Token BoldParse(string text, int i)
    {
        index = i;
        index++;

        if (i != 0 && i != text.Length - 2)
            IsHaveInsideBoldTag = true;
        if (IsEmptyStringInsideBold(text, i))
        {
            index = text.Length;
            return new Token("____");
        }

        if (IsBoldClosed)
        {
            if (!isValidOpenBoldTag(text, i+1))
            {
                return (new Token("__"));
            }
        }
        else
        {
            if (!isValidCloseBoldTag(text, i))
            {
                return new Token("__");
            }
        }


        if (tagsStack.Count > 1 &&
            tagsStack.Any(p => p.Type is {MarkdownTag: "_"}) &&
            tagsStack.Peek().Type is {MarkdownTag: "__"})
        {
            tagsStack.Pop();
        }
        if (!IsBoldClosed && i == text.Length - 1)
        {
            tagsStack.Pop();
        }
        var boldTag = addBoldTag(new BoldTag(), IsBoldClosed);
        IsBoldClosed = !IsBoldClosed;


        return boldTag;
    }

    public bool IsNextSymbolBold(string text, int i)
    {
        var nextIndex = i + 1;
        if (nextIndex < text.Length)
        {
            if (text[nextIndex] == '_')
            {
                return true;
            }
        }

        return false;
    }

    public int GetNewIndex() => index;

    private bool IsEmptyStringInsideBold(string text, int i)
    {
        int nextIndex = i + 1;
        if (nextIndex < text.Length - 2 && IsNextSymbolBold(text, nextIndex))
        {
            return true;
        }

        return false;
    }
}