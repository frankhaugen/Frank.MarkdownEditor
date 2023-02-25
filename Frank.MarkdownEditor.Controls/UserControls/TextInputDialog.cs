using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class TextInputDialog : Window
{
    private readonly TextBox _textInput;
    private readonly Button _okButton;
    private readonly TextDialogContext _dialogContext;

    public TextInputDialog(TextDialogContext dialogContext)
    {
        _dialogContext = dialogContext;

        _textInput = new TextBox();
        _textInput.Text = dialogContext.Text;
        _textInput.TextInput += (sender, args) =>
        {
            if (args.Text == "\r")
            {
                _okButton.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        };
        
        _okButton = new Button();
        _okButton.Content = "OK";
        _okButton.Click += OkButtonOnClick;
        
        Content = new StackPanel
        {
            Children =
            {
                _textInput,
                _okButton
            }
        };
    }

    private void OkButtonOnClick(object sender, RoutedEventArgs e)
    {
        _dialogContext.Text = _textInput.Text;
        Close();
    }
    
}

public class TextDialogContext
{
    public string Text { get; set; }
}