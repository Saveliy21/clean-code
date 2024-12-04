namespace Markdown.TagsType;

public class HeadingTag : ITagsType
{
    public string MarkdownTag { get; } = "#";
    public bool HasPair { get; set; } = true;

    public string GetHtmlOpenTag()
    {
        return "<h1>";
    }

    public string GetHtmlCloseTag()
    {
        return "</h1>";
    }
}