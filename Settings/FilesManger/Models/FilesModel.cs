namespace QuickFix.Settings.filesManger.Models
{
    public record FilesModel
    {
        public string CompanyFolder { get; set; }
        public string ModuleFolder { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
