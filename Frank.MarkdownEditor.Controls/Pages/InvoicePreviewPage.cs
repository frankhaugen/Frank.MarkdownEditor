using Frank.MarkdownEditor.Controls.UserControls;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class InvoicePreviewPage : Page
{
    private PdfViewer _pdfBrowser = new PdfViewer("Pdf") { Width = 700, MaxHeight = 800 };
    private CodeArea _jsonBlock = new CodeArea("Json");
    private StackPanel _panel = new StackPanel() { Orientation = Orientation.Vertical };


    public InvoicePreviewPage()
    {
        var previewButton = new Button();
        var generateButton = new Button();
        var sendButton = new Button();

        previewButton.Content = "Open PDF preview";
        previewButton.Click += PreviewButtonOnClick;

        generateButton.Content = "Generate Invoice";
        generateButton.Click += GenerateButtonOnClick;

        sendButton.Content = "Send Invoice";
        //sendButton.Visibility = Visibility.Collapsed;
        sendButton.Click += SendButtonOnClick;

        PdfFile = GeneratePdfName();

        _jsonBlock.Width = 256;
        _jsonBlock.MaxHeight = 256;

        _panel.Children.Add(generateButton);
        _panel.Children.Add(_jsonBlock);
        _panel.Children.Add(previewButton);
        _panel.Children.Add(sendButton);

        Content = _panel;
    }

    private void SendButtonOnClick(object sender, RoutedEventArgs e)
    {
    }

    public FileInfo PdfFile { get; set; }

    private FileInfo GeneratePdfName() => new FileInfo(Path.Combine(Path.GetTempPath(), $"{RandomNumberGenerator.GetInt32(64684, 668464686)}.pdf"));

    private void GenerateButtonOnClick(object sender, RoutedEventArgs e)
    {
        _pdfBrowser.Content = PdfFile;
    }

    private void PreviewButtonOnClick(object sender, RoutedEventArgs e)
    {
        var previewWindow = new Window();
        previewWindow.Name = "PdFPreview";
        previewWindow.Title = "PdFPreview";
        previewWindow.SizeToContent = SizeToContent.WidthAndHeight;
        previewWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        previewWindow.Content = new Frame() { Content = _pdfBrowser };

        previewWindow.Show();
    }
}                   