namespace QuickFix.Settings.FilesManger.Features.UplodeFiles.v1
{
    public record UplodeFileRespons
    {
        public List<string>? FilePathe { get; set; } = new List<string>();
        public int StatusCode { get; set; } = 200;
    }
}
