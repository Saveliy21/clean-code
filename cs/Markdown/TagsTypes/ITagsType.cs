namespace Markdown.TagsType;

public interface ITagsType
{
    public string MarkdownTag { get; }
    public bool HasPairTag { get; }
    public bool IsCloseTag { get; }
    public bool IsNeedCloseTag { get; }
    public string GetHtmlTag();
}