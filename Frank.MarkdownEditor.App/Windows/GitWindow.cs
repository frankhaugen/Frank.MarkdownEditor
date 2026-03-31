using System.Windows;
using System.Windows.Controls;
using Frank.MarkdownEditor.Controls.UserControls;
using Frank.MarkdownEditor.Core.Git;

namespace Frank.MarkdownEditor.App.Windows;

public class GitWindow : Window
{
    private readonly IGitRepositoryService _gitRepositoryService;

    public GitWindow(IGitRepositoryService gitRepositoryService)
    {
        _gitRepositoryService = gitRepositoryService;
        
        
        WindowStartupLocation = WindowStartupLocation.CenterScreen;
        Title = "Git History";
        SizeToContent = SizeToContent.WidthAndHeight;
        WindowStyle = WindowStyle.ToolWindow;
        ShowInTaskbar = false;
        Topmost = true;
        ShowActivated = true;
    }
    
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        var commitInfos = _gitRepositoryService.GetCommitInfo();
        var commitInfoExpanders = commitInfos.Select(x => new CommitInfoExpander(x)).ToList();
        var stackPanel = new StackPanel();
        foreach (var commitInfoExpander in commitInfoExpanders)
        {
            stackPanel.Children.Add(commitInfoExpander);
        }
        Content = stackPanel;
    }
}