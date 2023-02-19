using FontAwesome.Sharp;
using Frank.MarkdownEditor.Controls.Contexts;
using Frank.MarkdownEditor.Controls.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class TreePage : Page
{
    private readonly StackPanel _stackPanel = new() {Orientation = Orientation.Vertical};
    
    private readonly Menu _menu = new();
    private readonly TreeView _treeView = new();
    private readonly FileContext _fileContext;
    
    private readonly ILogger<TreePage> _logger;

    public TreePage(FileContext fileContext, ILogger<TreePage> logger)
    {
        _fileContext = fileContext;
        _logger = logger;

        BuildMenuItems();
        BuildTreeView();
        
        _stackPanel.MinWidth = 200;
        _stackPanel.Children.Add(_menu);
        _stackPanel.Children.Add(_treeView);

        Content = _stackPanel;
    }
    
    private async void BuildTreeView()
    {
        _treeView.Items.Clear();
        
        var files = FileContext.GetDictionary();
        
        var yearGroups = files.Keys.GroupBy(x => x.Year);

        foreach (var yearGroup in yearGroups)
        {
            var yearItem = new TreeViewItem()
            {
                Header = yearGroup.Key.ToString(),
                IsExpanded = true,
            };
            
            var weekGroups = yearGroup.GroupBy(x => x.Week);
            
            foreach (var weekGroup in weekGroups)
            {
                var weekItem = new TreeViewItem()
                {
                    Header = weekGroup.Key.ToString(),
                };
                
                foreach (var day in weekGroup)
                {
                    var file = files[day]!;
                    var item = new TreeViewItem()
                    {
                        Header = file!.DisplayName,
                    };

                    weekItem.Items.Add(item);
                }
                
                yearItem.Items.Add(weekItem);
            }
            _treeView.Items.Add(yearItem);
        }
        
        _treeView.SelectedItemChanged += TreeViewOnSelectedItemChanged;
    }

    private async void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (_treeView.SelectedItem is not TreeViewItem item) return;
        
        var files = FileContext.GetDictionary();
        var file = files.Values.FirstOrDefault(x => x!.DisplayName == item.Header.ToString());
        if (file is null) return;
        
        await _fileContext.Select(file);
    }

    private async void BuildMenuItems() => BuildRefreshMenuItem();

    private async void BuildRefreshMenuItem() => BuildMenuItem("Refresh", IconChar.Sync, (sender, args) => BuildTreeView());

    private async void BuildMenuItem(string header, IconChar icon, Action<object, RoutedEventArgs> action)
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
