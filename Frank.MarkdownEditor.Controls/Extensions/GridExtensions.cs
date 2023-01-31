using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class GridExtensions
{
    public static void AddChild(this Grid grid, UIElement child, int row, int column)
    {
        Grid.SetRow(child, row);
        Grid.SetColumn(child, column);
        grid.Children.Add(child);
    }

    public static void GenerateGridRowsAndColumns(this Grid grid, int rows, int columns)
    {
        for (var i = 0; i < columns; i++)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        }

        for (var i = 0; i < rows; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }
    }
}