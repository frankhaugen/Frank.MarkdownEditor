using Frank.MarkdownEditor.Controls.UserControls;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Frank.MarkdownEditor.Controls.Pages;

public class AuthenticationPage : Page
{
    private readonly ILogger<AuthenticationPage> _logger;

    private readonly StackPanel _panel = new StackPanel();
    private readonly Button _button = new Button() { Name = "AuthenticationButton", Content = "Authenticate" };
    private readonly TextArea _label;

    public AuthenticationPage(ILogger<AuthenticationPage> logger)
    {
        _logger = logger;

        _label = new TextArea("Token response", "");

        _panel.Children.Add(new TextInput(nameof(Username), Username, UsernameChanged));
        _panel.Children.Add(new TextInput(nameof(Password), Password, PasswordChanged));
        _panel.Children.Add(new TextInput(nameof(TokenUrl), TokenUrl, TokenUrlChanged));
        _panel.Children.Add(new TextInput(nameof(BaseUrl), BaseUrl, BaseUrlChanged));
        _panel.Children.Add(new TextInput(nameof(Scope), Scope, ScopeChanged));
        _panel.Children.Add(new TextInput(nameof(ClientSecret), ClientSecret, ClientSecretChanged));

        _panel.Children.Add(_label);

        _button.Click += ButtonOnClick;

        _panel.Children.Add(_button);

        MaxWidth = 256;

        Content = _panel;
    }
    //public void RegisterAuthenticationCallback()



    private async void ButtonOnClick(object sender, RoutedEventArgs e)
    {
        _logger.LogInformation("Authenticating");
        try
        {
            _logger.LogInformation(_label.Content);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            MessageBox.Show(JsonSerializer.Serialize(exception, new JsonSerializerOptions() { WriteIndented = true }));
        }
    }


    private string Username { get; set; }
    private string Password { get; set; }
    private string ClientSecret { get; set; }
    private string Scope { get; set; }
    private string TokenUrl { get; set; }
    private string BaseUrl { get; set; }

    private void UsernameChanged(object arg1, TextChangedEventArgs arg2) => Username = (arg1 as TextBox)?.Text ?? "";
    private void PasswordChanged(object arg1, TextChangedEventArgs arg2) => Password = (arg1 as TextBox)?.Text ?? "";
    private void BaseUrlChanged(object arg1, TextChangedEventArgs arg2) => BaseUrl = (arg1 as TextBox)?.Text ?? "";
    private void ScopeChanged(object arg1, TextChangedEventArgs arg2) => Scope = (arg1 as TextBox)?.Text ?? "";
    private void TokenUrlChanged(object arg1, TextChangedEventArgs arg2) => TokenUrl = (arg1 as TextBox)?.Text ?? "";
    private void ClientSecretChanged(object arg1, TextChangedEventArgs arg2) => ClientSecret = (arg1 as TextBox)?.Text ?? "";
}