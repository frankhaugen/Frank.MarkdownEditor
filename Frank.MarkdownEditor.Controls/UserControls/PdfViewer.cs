using System;
using System.IO;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class PdfViewer : GroupBox
{
    private readonly WebBrowser _content = new();

    public PdfViewer(string header)
    {
        Header = header;
        base.Content = _content;
    }

    public PdfViewer(string header, FileInfo pdfFile)
    {
        Header = header;
        _content.Source = new Uri(pdfFile.FullName);
        base.Content = _content;
    }

    public new FileInfo Content
    {
        get => new(_content.Source.OriginalString);
        set => _content.Source = new Uri(value.FullName);
    }
}