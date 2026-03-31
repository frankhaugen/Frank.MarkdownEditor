using Frank.MarkdownEditor.Core.Git;
using LibGit2Sharp;

namespace Frank.MarkdownEditor.Core.Extensions;

public static class GitRepositoryConfigurationExtensions
{
    public static DirectoryInfo GetRepositoryDirectory(this GitRepositoryConfiguration configuration) =>
        new(Path.Combine(configuration.WorkDirectory.FullName, configuration.RepositoryName));
    
    public static UsernamePasswordCredentials ToUsernamePasswordCredentials(this GitRepositoryConfiguration configuration) =>
        new()
        {
            Username = configuration.Username.Address,
            Password = configuration.Password
        };

    public static Signature ToSignature(this GitRepositoryConfiguration configuration) => new(configuration.Username.Address, configuration.Username.Address, DateTimeOffset.UtcNow);

    public static CloneOptions ToCloneOptions(this GitRepositoryConfiguration configuration) =>
        new()
        {
            BranchName = configuration.Branch,
            CredentialsProvider = (_, _, _) => configuration.ToUsernamePasswordCredentials(),
            Checkout = true,
            FetchOptions = configuration.ToFetchOptions()
        };
	
    public static PushOptions ToPushOptions(this GitRepositoryConfiguration configuration) =>
        new()
        {
            CredentialsProvider = (_, _, _) => configuration.ToUsernamePasswordCredentials()
        };
	
    public static PullOptions ToPullOptions(this GitRepositoryConfiguration configuration) =>
        new()
        {
            FetchOptions = configuration.ToFetchOptions(),
            MergeOptions = configuration.ToMergeOptions()
        };
	
    public static FetchOptions ToFetchOptions(this GitRepositoryConfiguration configuration) =>
        new()
        {
            CredentialsProvider = (_, _, _) => configuration.ToUsernamePasswordCredentials()
        };
    
    public static MergeOptions ToMergeOptions(this GitRepositoryConfiguration configuration) =>
        new()
        {
            FileConflictStrategy = CheckoutFileConflictStrategy.Theirs
        };
}