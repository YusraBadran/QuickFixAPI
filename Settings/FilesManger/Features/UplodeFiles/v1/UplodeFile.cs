using QuickFix.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using QuickFix.Settings.filesManger.Models;

namespace QuickFix.Settings.FilesManger.Features.UplodeFiles.v1;

public record UplodeFile : FilesModel, ICommand<UplodeFileRespons>
{
    public UplodeFile(FilesModel request) : base(request)
    {
    }
}
public class Validator : AbstractValidator<UplodeFile>
{
    public Validator()
    {

        RuleFor(s => s.ModuleFolder).NotEmpty().WithMessage(" يجب تحديد نوع المجلد المراد الرفع اليه ");
        RuleFor(s => s.Files).NotEmpty().WithMessage(" لايمكن اضفة ملف فارغ ");
    }
}
public class UploadFileHandler : ICommandHandler<UplodeFile, UplodeFileRespons>
{

    public static IWebHostEnvironment _WebHostEnvironment;

    public UploadFileHandler(IWebHostEnvironment WebHostEnvironment)
    {
        _WebHostEnvironment = WebHostEnvironment;
    }

    public async Task<UplodeFileRespons> Handle(UplodeFile request, CancellationToken cancellationToken)
    {
        var resultPath = $"Upload/{request.ModuleFolder}/";
        var returnPath = new UplodeFileRespons();
        string path = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\Upload\{request.ModuleFolder}\");
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        try
        {
            foreach (var item in request.Files)
            {
                using (FileStream fileStream = File.Create(path + item.FileName))
                {
                    item.CopyTo(fileStream);
                    fileStream.Flush();
                    resultPath += item.FileName;
                }
                returnPath.FilePathe.Add(resultPath);
                resultPath = $"Upload/{request.ModuleFolder}/";
            }
        }
        catch
        {
            returnPath.StatusCode = 500;
            return returnPath;
        }
        returnPath.StatusCode = 200;
        return returnPath;
    }
}
