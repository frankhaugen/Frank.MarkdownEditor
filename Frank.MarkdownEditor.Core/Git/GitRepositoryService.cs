using Frank.MarkdownEditor.Core.Extensions;
using LibGit2Sharp;
using Microsoft.Extensions.Options;

namespace Frank.MarkdownEditor.Core.Git;

public class GitRepositoryService : IGitRepositoryService
{
	private readonly IOptionsSnapshot<GitRepositoryConfiguration> _options;

	public GitRepositoryService(IOptionsSnapshot<GitRepositoryConfiguration> options)
	{
		_options = options;
		
		EnsureRepository();
	}

	private void EnsureRepository()
	{
		if (!Repository.IsValid(_options.Value.GetRepositoryDirectory().FullName))
		{
			Repository.Clone(_options.Value.Uri.AbsoluteUri, _options.Value.GetRepositoryDirectory().FullName, _options.Value.ToCloneOptions());
		}
		else
		{
			using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
			Commands.Pull(repository, _options.Value.ToSignature(), _options.Value.ToPullOptions());
		}
	}
	
	public void Stage(FileInfo file)
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		Commands.Stage(repository, file.FullName);
	}
	
	public void Stage(DirectoryInfo directory)
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		Commands.Stage(repository, Path.Combine(directory.FullName, "*"));
	}
	
	public void Stage()
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		Commands.Stage(repository, "*");
	}
	
	public void Commit(string message)
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		repository.Commit(message, _options.Value.ToSignature(), _options.Value.ToSignature());
	}
	
	public void Push()
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		repository.Network.Push(repository.Head, _options.Value.ToPushOptions());
	}

	public IEnumerable<CommitInfo> GetCommitInfo()
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		return repository.Commits
			.QueryBy(new CommitFilter { SortBy = CommitSortStrategies.Time })
			.Select(commit => commit.ToCommitInfo())
			.ToList();
	}
	
	public IEnumerable<CommitInfo> GetCommitInfo(FileInfo file)
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		return repository.Commits
			.QueryBy(new CommitFilter { SortBy = CommitSortStrategies.Time })
			.Where(commit => commit.Tree[file.FullName] != null &&
			                 commit.Parents.Any() &&
			                 commit.Parents.First().Tree[file.FullName] == null)
			.Select(commit => commit.ToCommitInfo())
			.ToList();
	}
	
	public void ReturnFileToCommit(FileInfo file, CommitInfo commit)
	{
		using var repository = new Repository(_options.Value.GetRepositoryDirectory().FullName);
		var commitToReturnTo = repository.Commits.Single(c => c.Sha == commit.Hash);
		if (commitToReturnTo[file.FullName].Target is not Blob blob) return;
		file.WriteAllText(blob.GetContentText());
	}
	
	public void StageCommitAndPush(FileInfo file, string? message = null)
	{
		Stage(file);
		Commit(message ?? $"Updated {file.Name}");
		Push();
	}
}