using Markdown.TagsType;

namespace Markdown;

public class Token
{
    public bool IsTag { get; }
    public int StartIndex { get; }
    public int Length { get; }

    // Если Type = null, значит этот токен для текста, иначе - для тэга.
    public ITagsType? Type { get; }

    public Token(bool isTag, int startIndex, int length, ITagsType? type = null)
    {
        IsTag = isTag;
        StartIndex = startIndex;
        Length = length;
        Type = type;
    }
}