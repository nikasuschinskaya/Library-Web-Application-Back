using Library.Domain.Interfaces;

namespace Library.Infrastucture.Storages;

public class FileSystemStorage : IFileStorage
{
    private readonly string _baseDirectory;

    public FileSystemStorage(string baseDirectory) => _baseDirectory = baseDirectory;

    public async Task<string> SaveFileAsync(string fileName, Stream fileStream)
    {
        var filePath = Path.Combine(_baseDirectory, fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(filePath) ?? string.Empty);

        await using (var fileStreamOutput = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, true))
        {
            await fileStream.CopyToAsync(fileStreamOutput);
        }

        return filePath;
    }

    public async Task<Stream> GetFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("File not found", filePath);

        return await Task.FromResult(new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true));
    }

    public async Task DeleteFileAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            await Task.Run(() => File.Delete(filePath));
        }
    }
}

