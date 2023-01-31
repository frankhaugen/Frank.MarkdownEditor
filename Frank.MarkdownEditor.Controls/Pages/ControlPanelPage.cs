using Frank.MarkdownEditor.Controls.UserControls;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class ControlPanelPage : Page
{
    private readonly StackPanel _stackPanel = new();

    public ControlPanelPage()
    {
        Content = _stackPanel;
        Width = 64;
    }

    public void AddButton(ButtonInput buttonInput) => _stackPanel.Children.Add(buttonInput);
    public void AddButtonGroup(ButtonInputGroup buttonGroup) => _stackPanel.Children.Add(buttonGroup);
}