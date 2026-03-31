using Frank.MarkdownEditor.Core.Git;
using LibGit2Sharp;

namespace Frank.MarkdownEditor.Core.Extensions;

public static class CommitExtensions
{
    public static CommitInfo ToCommitInfo(this Commit commit) => new(commit.Sha, commit.MessageShort, commit.Message, commit.Author.Name, commit.Author.When);
}