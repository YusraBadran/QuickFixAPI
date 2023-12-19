namespace QuickFix.Shared.Module
{
    public class SuccessRequest
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
    }
}
