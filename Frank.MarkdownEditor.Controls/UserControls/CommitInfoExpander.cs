using System.Windows.Controls;
using Frank.MarkdownEditor.Core.Git;

namespace Frank.MarkdownEditor.Controls.UserControls;

public class CommitInfoExpander : Expander
{
    private readonly CommitInfo _commitInfo;

    public CommitInfoExpander(CommitInfo commitInfo)
    {
        _commitInfo = commitInfo;
    }
    
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        Header = new Label() { Content = $"{_commitInfo.Timestamp:s} - {_commitInfo.ShortMessage} - {_commitInfo.Author}" };
        Content = _commitInfo.Message;
    }
}