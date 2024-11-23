namespace Markdown.TagsType;

public class BoldTag : ITagsType
{
    public string MarkdownTag { get; }
    public bool HasPairTag { get; }
    public bool IsCloseTag { get; }
    public bool IsNeedCloseTag { get; }

    public BoldTag(bool isCloseTag, bool hasPairTag)
    {
        MarkdownTag = "__";
        IsCloseTag = isCloseTag;
        HasPairTag = hasPairTag;
        IsNeedCloseTag = true;
    }

    public string GetHtmlTag()
    {
        return IsCloseTag ? "</strong>" : "<strong>";
    }
}