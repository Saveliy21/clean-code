﻿namespace Markdown.TagsType;

public class LinkTag : ITagsType
{
    public string MarkdownTag { get; } = "[]()";
    public bool HasPair { get; set; } = true;

    public string LinkName = "";
    public string LinkUrl = "";
    public string GetHtmlOpenTag()
    {
        return "<a href=\"";
    }

    public string GetHtmlCloseTag()
    {
        return "</a>";
    }
}