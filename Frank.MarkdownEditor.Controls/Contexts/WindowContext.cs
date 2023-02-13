using System;
using System.Windows;

namespace Frank.MarkdownEditor.Controls.Contexts;

public class WindowContext
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string ClassName { get; set; }
    
    public Size Size
    {
        get => Size;
        set => SizeChanged?.Invoke(value);
    }

    public event Action<Size> SizeChanged;
    
    public Point Location { get; set; }
    public WindowState WindowState { get; set; }
    

    public ScreenContext OwnerScreen { get; set; }
}