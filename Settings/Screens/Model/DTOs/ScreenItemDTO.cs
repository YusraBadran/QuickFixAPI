namespace QuickFix.Settings.Screens.Model.DTOs
{
    public class ScreenItemDTO
    {
        public Guid Id { get; set; }
        public string label { get; set; }
        public string icon { get; set; }
        public string translate { get; set; }
        public string? routerLink { get; set; }
        public Guid? SubId { get; set; }
    }
}
