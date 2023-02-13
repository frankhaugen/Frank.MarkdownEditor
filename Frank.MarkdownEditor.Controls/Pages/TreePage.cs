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
    
    private void BuildTreeView()
    {
        _treeView.Items.Clear();
        
        var files = _fileContext.GetDictionary();
        
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

                    item.Selected += ItemOnSelected;
                    
                    weekItem.Items.Add(item);
                }
                
                yearItem.Items.Add(weekItem);
            }
            _treeView.Items.Add(yearItem);
        }
    }

    private void ItemOnSelected(object sender, RoutedEventArgs e)
    {
        if (sender is not TreeViewItem item) return;
        if (item.Header is not string header) return;
        
        var files = _fileContext.GetDictionary();
        var file = files.Values.FirstOrDefault(x => x!.DisplayName == header);
        if (file is null) return;
        
        _fileContext.Select(file).GetAwaiter().GetResult();
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
