namespace Markdown.TagsType;

public class BoldTag : ITagsType
{
    public string MarkdownTag { get; } = "__";
    public bool HasPair { get; set; }

    public string GetHtmlOpenTag()
    {
        return "<strong>";
    }

    public string GetHtmlCloseTag()
    {
        return "</strong>";
    }
}