using System;
using System.Collections.Generic;

namespace Frank.MarkdownEditor.Controls.Extensions;

public interface IUrl
{
    IUrl AppendCompanyIdSegments(Guid organizationId, Guid? clientId = null);
    IUrl AppendSegments(params string[] segments);
    IUrl AppendParameters(params KeyValuePair<string, string>[] parameters);
    Uri ToUri();
    string ToString();
}