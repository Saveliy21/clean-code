using Markdown.TagsType;

namespace Markdown;

public class Token
{
    public int StartIndex { get; }
    public int Length { get; }

    // Если Type = null, значит этот токен для текста, иначе - для тэга.
    public ITagsType? Type { get; }

    public Token(int startIndex, int length, ITagsType? type = null)
    {
        StartIndex = startIndex;
        Length = length;
        Type = type;
    }
}