using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using QuickFix.Settings.filesManger.Models;

namespace QuickFix.Settings.FilesManger.Features.UplodeFiles.v1;

public class UplodeFileController : Controller
{
    private readonly ILogger<UplodeFileController> _logger;
    private readonly ICommandProcessor _sender;
    private readonly CancellationToken _cancellationToken;
    public UplodeFileController(ILogger<UplodeFileController> logger, ICommandProcessor sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [Route("api/fileManger/upload/v1")]
    [ApiExplorerSettings(GroupName = "fileManger")]
    [HttpPost, DisableRequestSizeLimit]
    public async Task<ActionResult<FilesModel>> UplodeFileMeth(string companyFolder, string moduleFolder, [FromForm] List<IFormFile> file)
    {
        var request = new FilesModel()
        {

            ModuleFolder = moduleFolder,
            Files = file
        };
        var result = await _sender.SendAsync(new UplodeFile(request));
        return Ok(result.FilePathe);
    }
}
