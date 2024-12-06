using System.Text;
using Markdown.Parsers;
using Markdown.TagsType;

namespace Markdown;

public class Preparator
{
    private readonly ListOfTokens<Token> tokens = new();

    private readonly BoldParser boldParser;
    private readonly ItalicParser italicParser;
    private  LinkParser linkParser;
    private bool isHeadUp;
    private int countOfLastEddedTokens = 0;
    private readonly Stack<Token> tagsStack;
    private readonly SlashParser slashParser;

    public Preparator()
    {
        this.linkParser = new LinkParser();
        boldParser = new BoldParser();
        italicParser = new ItalicParser();
        slashParser = new SlashParser();
        tagsStack = GeneralParser.tagsStack;
    }

    public void Paragrapher(string text)
    {
        var paragraphs = text.Split('\n');
        foreach (var line in paragraphs)
        {
            Tokenize(line);
            tagsStack.Clear();
        }
        tokens.Remove(tokens.Last());
        CheckTokens();
    }


    public List<Token> GetTokens()
    {
        return tokens;
    }

    private void Tokenize(string line)
    {
        var words = line.Split(' ');
        foreach (var token in words)
        {
            AddToken(token);
            if(tokens.Count != 0)
                tokens.Add(new Token(" "));
        }

        tokens.Remove(tokens.Last());
        if (isHeadUp)
        {
            isHeadUp = false;
            tokens.Add(new Token(new HeadingTag().GetHtmlCloseTag()));
        }

        tokens.Add(new Token("\n"));
    }


    private void AddToken(string text)
    {
        var isHaveDigitInside = false;
        if (linkParser.IsThisLink)
        {
            tokens.Add(linkParser.LinkParse(text));
            return;
        }
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case '_':
                    if (boldParser.IsNextSymbolBold(text,i))
                    {
                        tokens.Add(boldParser.BoldParse(text,i));
                        i = boldParser.GetNewIndex();
                        break;
                    }
                    tokens.Add(italicParser.ItalicParse(text,i));
                    break;
                case '#':
                    tokens.Add(new Token(new HeadingTag().GetHtmlOpenTag()));
                    isHeadUp = true;
                    return;
                case '[':
                    tokens.Add(linkParser.LinkParse(text));
                    return;
                case '\\':
                    tokens.Add(slashParser.QuotedToken(text, i, boldParser.IsNextSymbolBold(text,i)));
                    i = slashParser.GetIndex();
                    break;
                default:
                    if (Char.IsDigit(text[i]))
                        isHaveDigitInside = true;
                    tokens.Add(new Token(text[i].ToString()));
                    break;
            }
        }
        countOfLastEddedTokens = tokens.Count - countOfLastEddedTokens;
        GetValidResult(isHaveDigitInside, italicParser.IsHaveInsideItalicTag, boldParser.IsHaveInsideBoldTag);
    }

    private void GetValidResult(bool isHaveDigitInside, bool isHaveInsideItalicTag, bool
        isHaveIndsideBoldTag)
    {
        if (isHaveDigitInside)
            MarkLastTokensToMarkdown();
        if (isHaveInsideItalicTag && tagsStack.Count != 0)
        {
            tagsStack.Pop();
        }

        if (isHaveIndsideBoldTag && tagsStack.Count != 0)
        {
            tagsStack.Pop();
        }
    }



    private void CheckTokens()
    {
        foreach (var token in tokens)
        {
            if (token.Type != null && !token.Type.HasPair)
            {
                token.Context = token.Type.MarkdownTag;
            }
        }
    }

    private void MarkLastTokensToMarkdown()
    {
        for (int i = tokens.Count - countOfLastEddedTokens; i < tokens.Count; i++)
        {
            if (tokens[i].Type != null)
            {
                tokens[i].Type!.HasPair = false;
            }
        }
    }
    
}