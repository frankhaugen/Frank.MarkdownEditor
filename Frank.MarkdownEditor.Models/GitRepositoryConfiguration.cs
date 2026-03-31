using System.Net.Mail;

namespace Frank.MarkdownEditor.Core.Git;

public class GitRepositoryConfiguration
{
    public string RepositoryName { get; set; }
	
    public Uri Uri { get; set; }
	
    public DirectoryInfo WorkDirectory { get; set; }
	
    public MailAddress Username { get; set; }
	
    public string Password { get; set; }
    
    public string Branch { get; set; } = "main";
}