using LibGit2Sharp;

namespace Frank.MarkdownEditor.Core.Git;

public interface IGitRepositoryService
{
    void Stage(FileInfo file);
    void Stage(DirectoryInfo directory);
    void Stage();
    void Commit(string message);
    void Push();
    IEnumerable<CommitInfo> GetCommitInfo();
    IEnumerable<CommitInfo> GetCommitInfo(FileInfo file);
    void ReturnFileToCommit(FileInfo file, CommitInfo commit);
    void StageCommitAndPush(FileInfo file, string? message = null);
}