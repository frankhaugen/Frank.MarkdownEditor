using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Frank.MarkdownEditor.Controls.Contexts;

// ReSharper disable once InconsistentNaming
public  static partial class FileInfoExtensions
{
    
    private static readonly ReaderWriterLock locker = new();
    
    public static async Task WriteAllTextAsync(this FileInfo file, string text)
    {
        try
        {
            locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
            file.Directory?.Create();
            await File.WriteAllTextAsync(file.FullName, text);
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }
    
    public static async Task AppendAllTextAsync(this FileInfo file, string text)
    {
        try
        {
            locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
            file.Directory?.Create();
            await File.AppendAllTextAsync(file.FullName, text);
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }
    
    public static async Task WriteAllTextAsJsonAsync<T>(this FileInfo file, T obj)
    {
        try
        {
            locker.AcquireWriterLock(int.MaxValue); //You might wanna change timeout value 
            file.Directory?.Create();
            var json = JsonSerializer.Serialize(obj, JsonOptions.FileDefault);
            await File.WriteAllTextAsync(file.FullName, json);
        }
        finally
        {
            locker.ReleaseWriterLock();
        }
    }
    
    public static async Task<string> ReadAllTextAsync(this FileInfo file, CancellationToken cancellationToken = default)
    {
        try
        {
            locker.AcquireReaderLock(int.MaxValue);
            return await File.ReadAllTextAsync(file.FullName, cancellationToken);
        }
        finally
        {
            locker.ReleaseReaderLock();
        }
    }
    
    public static async Task<IEnumerable<string>> ReadAllLinesAsync(this FileInfo file, CancellationToken cancellationToken = default)
    {
        try
        {
            locker.AcquireReaderLock(int.MaxValue);
            return await File.ReadAllLinesAsync(file.FullName, cancellationToken);
        }
        finally
        {
            locker.ReleaseReaderLock();
        }
    }
    
    public static async Task<T> ReadAllTextAsJsonAsync<T>(this FileInfo file, CancellationToken cancellationToken = default)
    {
        try
        {
            locker.AcquireReaderLock(int.MaxValue);
            var json = await File.ReadAllTextAsync(file.FullName, cancellationToken);
            return JsonSerializer.Deserialize<T>(json, JsonOptions.FileDefault);
        }
        finally
        {
            locker.ReleaseReaderLock();
        }
    }
}