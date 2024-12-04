using System.Collections;
using Markdown.TagsType;

namespace Markdown;

public class Token 
{
    public string Context { get; set; }
    public int Length => Context.Length;
    public static int IdCounter = 0;
    public readonly int Id;

    // Если Type = null, значит этот токен для текста, иначе - для тэга.
    public ITagsType? Type { get; }

    public Token(string context, ITagsType? type = null)
    {
        Context = context;
        Type = type;
        Id = IdCounter++;
    }
    
}