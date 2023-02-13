using System.Globalization;
using System.Numerics;
using System.Windows.Forms;

namespace Frank.MarkdownEditor.Controls.Extensions;

public static class NumberExtensions
{
    public static Label ToLabel<T>(this T number) where T : INumber<T> =>
        new Label 
        {
            Text = number.ToString("00", CultureInfo.InvariantCulture) 
        };
}