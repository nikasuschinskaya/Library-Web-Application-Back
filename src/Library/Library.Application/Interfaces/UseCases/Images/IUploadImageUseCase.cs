namespace Library.Application.Interfaces.UseCases.Images;

public interface IUploadImageUseCase : IUseCase
{
    Task<string> ExecuteAsync(string fileName, Stream fileStream);
}
