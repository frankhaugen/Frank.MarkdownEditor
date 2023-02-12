using Frank.MarkdownEditor.Controls.UserControls;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class PreviewPage : Page
{
    private readonly WebBrowser _browser = new();

    public PreviewPage()
    {
        _browser.Source = new(@"C:\repos\frankhaugen\Frank.MarkdownEditor\Frank.MarkdownEditor.App\preview.html");
        Content = _browser;
        
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
    }

}