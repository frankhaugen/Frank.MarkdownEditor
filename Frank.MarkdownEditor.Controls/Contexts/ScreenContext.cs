using System.Windows;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class ScreenContext
{
    public string Name { get; set; }
    public string DeviceName { get; set; }
    public bool Primary { get; set; }
    public Size Size { get; set; }
    public Point Location { get; set; }
}