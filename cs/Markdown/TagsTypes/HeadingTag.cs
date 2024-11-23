namespace Markdown.TagsType;

public class HeadingTag : ITagsType
{
    public string MarkdownTag { get; }
    public bool HasPairTag { get; }
    public bool IsCloseTag { get; }
    public bool IsNeedCloseTag { get; }

    public HeadingTag(bool isCloseTag, bool hasPairTag)
    {
        MarkdownTag = "#";
        IsCloseTag = isCloseTag;
        HasPairTag = hasPairTag;
        IsNeedCloseTag = false;
    }

    public string GetHtmlTag()
    {
        return IsCloseTag ? "</h1>" : "<h1>";
    }
}