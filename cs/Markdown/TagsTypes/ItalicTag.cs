namespace Markdown.TagsType;

public class ItalicTag : ITagsType
{
    public string MarkdownTag { get; }
    public bool HasPairTag { get; }
    public bool IsCloseTag { get; }
    public bool IsNeedCloseTag { get; }

    public string GetHtmlTag()
    {
        return IsCloseTag ? "</em>" : "<em>";
    }

    public ItalicTag(bool isCloseTag, bool hasPairTag)
    {
        MarkdownTag = "_";
        IsCloseTag = isCloseTag;
        HasPairTag = hasPairTag;
        IsNeedCloseTag = true;
    }
}