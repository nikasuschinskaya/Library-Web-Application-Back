using Library.Application.Interfaces.UseCases.Images;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImageController : ControllerBase
{
    private readonly IUploadImageUseCase _uploadImageUseCase;

    public ImageController(IUploadImageUseCase uploadImageUseCase)
    {
        _uploadImageUseCase = uploadImageUseCase;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file)
    {
        await using (var fileStream = file.OpenReadStream())
        {
            var savedFilePath = await _uploadImageUseCase.ExecuteAsync(file.FileName, fileStream);
            return Ok(new { FilePath = savedFilePath });
        }
    }
}
