using Frank.MarkdownEditor.Controls.Extensions;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class PropertiesList<T> : GroupBox
{
    private readonly Grid _content;

    public PropertiesList()
    {
        Header = typeof(T).Name;
        _content = new Grid();
        base.Content = _content;
    }

    public void Update(T source)
    {
        var stack = new StackPanel();
        var properties = source.GetPropertiesAndValues();

        foreach (var keyValuePair in properties)
        {
            stack.Children.Add(new TextLabel(keyValuePair.Key, keyValuePair.Value));
        }

        _content.Children.Clear();
        _content.Children.Add(stack);
    }

}