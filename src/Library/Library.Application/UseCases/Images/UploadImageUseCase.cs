using Library.Application.Interfaces.UseCases.Images;
using Library.Domain.Interfaces;

namespace Library.Application.UseCases.Images;

public class UploadImageUseCase : IUploadImageUseCase
{
    private readonly IFileStorage _fileStorage;

    public UploadImageUseCase(IFileStorage fileStorage)
    {
        _fileStorage = fileStorage;
    }

    public async Task<string> ExecuteAsync(string fileName, Stream fileStream)
    {
        if (fileStream == null || fileStream.Length == 0)
            throw new ArgumentException("Invalid file stream");

        return await _fileStorage.SaveFileAsync(fileName, fileStream);
    }
}
