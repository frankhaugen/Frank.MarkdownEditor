using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class CellGrid : Grid
{
    private readonly Cell[,] _cells;

    public CellGrid(int columns, int rows)
    {
        _cells = new Cell[columns, rows];

        for (var i = 0; i < columns; i++)
        {
            ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
        }
        for (var i = 0; i < rows; i++)
        {
            RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        }

        for (var i = 0; i < columns; i++)
        {
            for (var j = 0; j < rows; j++)
            {
                _cells[i, j] = new Cell(new CellPosition(i, j));
            }
        }

        foreach (var cell in _cells)
        {
            Children.Add(cell);
        }
    }

    public void SetCellContent<T>(int column, int row, T page) where T : Page => SetCellContent(new CellPosition(column, row), page);

    public void SetCellContent<T>(CellPosition position, T page) where T : Page
    {
        var cell = _cells[position.Column, position.Roww];
        cell.Navigate(page);
    }
}