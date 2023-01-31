namespace Frank.MarkdownEditor.Controls.Extensions;

public static class GenerExtensions
{
    public static T As<T>(this object source) where T : class => (T)source;



}