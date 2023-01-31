using Frank.MarkdownEditor.Controls.Extensions;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class Cell : Frame
{
    private CellPosition _position;

    public Cell(CellPosition position)
    {
        SetPosition(position);
    }

    public CellPosition GetPosition() => _position;

    public void SetPosition(CellPosition position)
    {
        _position = position;
        this.SetGridPosition(_position.Column, _position.Roww);
    }
}