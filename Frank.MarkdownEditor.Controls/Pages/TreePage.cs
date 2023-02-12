using FontAwesome.Sharp;
using Frank.MarkdownEditor.Controls.Contexts;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class TreePage : Page
{
    private readonly StackPanel _stackPanel = new() {Orientation = Orientation.Vertical};
    
    private readonly Menu _menu = new();
    private readonly TreeView _treeView = new();
    private readonly FileContext _fileContext;
    public TreePage(FileContext fileContext)
    {
        _fileContext = fileContext;
        
        BuildMenuItems();
        BuildTreeView();
        
        _stackPanel.Children.Add(_menu);
        _stackPanel.Children.Add(_treeView);
        
        foreach (var stackPanelChild in _stackPanel.Children)
        {
            // stretch the children to fill the stackpanel
            ((FrameworkElement) stackPanelChild).HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            ((FrameworkElement) stackPanelChild).VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
        }
        
        HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
        VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
        Content = _stackPanel;
    }
    
    private void BuildTreeView()
    {
        _treeView.Items.Clear();
        
        var root = new TreeViewItem()
        {
            Header = "Root",
            IsExpanded = true
        };
        
        _treeView.Items.Add(root);
        
        var files = _fileContext.GetFiles();
        
        var years = files.GroupBy(f => f.Year);
        
        foreach (var year in years)
        {
            var yearItem = new TreeViewItem()
            {
                Header = year.Key,
                IsExpanded = true
            };
            root.Items.Add(yearItem);
            
            var weeks = year.GroupBy(f => f.Week);
            foreach (var week in weeks)
            {
                var days = week.ToList();
                var daysItem = new TreeViewItem()
                {
                    Header = $"{year.Key} - {week.Key}",
                    IsExpanded = true
                };
                yearItem.Items.Add(daysItem);
                
                foreach (var day in days)
                {
                    var item = new TreeViewItem()
                    {
                        Header = day.Day,
                        Tag = day
                    };
        
                    item.Selected += (sender, args) => _fileContext.Select(day);
                    daysItem.Items.Add(item);
                }
            }
        }
    }
















    private void BuildMenuItems()
    {
        BuildRefreshMenuItem();
    }

    private void BuildRefreshMenuItem() => BuildMenuItem("Refresh", IconChar.Sync, (sender, args) => BuildTreeView());

    private void BuildMenuItem(string header, IconChar icon, Action<object, RoutedEventArgs> action)
    {
        var item = new MenuItem()
        {
            Header = header,
            Icon = icon.ToImageSource(),
        };
        
        item.Click += (sender, args) => action(sender, args);
        
        _menu.Items.Add(item);
    }
}
