using System;
using System.Collections.Generic;
using System.Text;

namespace Frank.MarkdownEditor.Controls.Extensions;

public class Url : IUrl
{
    private readonly string _baseUrl;
    private readonly StringBuilder _urlBuilder = new();
    private readonly StringBuilder _parametersBuilder = new();

    private Url(string baseUrl = "") => _baseUrl = baseUrl;

    public static Url CreateInstance(string baseUrl = "") => new(baseUrl);

    public IUrl AppendCompanyIdSegments(Guid organizationId, Guid? clientId = null)
    {
        if (organizationId == Guid.Empty) throw new ArgumentException("Guid cannot be of value 'Empty'", nameof(organizationId));

        _urlBuilder.Append("organizations");
        _urlBuilder.Append('/');
        _urlBuilder.Append(organizationId);

        if (clientId == null || clientId == Guid.Empty) return this;

        _urlBuilder.Append('/');
        _urlBuilder.Append("clients");
        _urlBuilder.Append('/');
        _urlBuilder.Append(clientId);

        return this;
    }

    public IUrl AppendSegments(params string[] segments)
    {
        foreach (var segment in segments)
        {
            _urlBuilder.Append('/');
            _urlBuilder.Append(segment);
        }

        return this;
    }

    public IUrl AppendParameters(params KeyValuePair<string, string>[] parameters)
    {
        foreach (var parameter in parameters)
        {
            _parametersBuilder.Append(parameter.Key);
            _parametersBuilder.Append('=');
            _parametersBuilder.Append(parameter.Value);
            _parametersBuilder.Append('&');
        }

        return this;
    }

    public Uri ToUri()
    {
        var url = ToString();
        return !string.IsNullOrWhiteSpace(_baseUrl) ? new Uri(url) : new Uri(url, UriKind.Relative);
    }

    public override string ToString()
    {
        var url = _urlBuilder.ToString();
        if (!string.IsNullOrWhiteSpace(_baseUrl)) url = _baseUrl + "/" + url;
        if (_parametersBuilder.Length > 0)
        {
            url += "?" + _parametersBuilder;
        }

        return url;
    }
}