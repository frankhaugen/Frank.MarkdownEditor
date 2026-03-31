namespace Frank.MarkdownEditor.Core.Git;

public record CommitInfo(string Hash, string ShortMessage, string Message, string Author, DateTimeOffset Timestamp);