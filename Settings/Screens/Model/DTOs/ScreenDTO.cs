namespace QuickFix.Settings.Screens.Model.DTOs
{
    public class ScreenDTO
    {
        public Guid Id { get; set; }
        public string label { get; set; }
        public string Icon { get; set; }
        public string IconImge { get; set; }
        public string Translate { get; set; }
        public string? RouterLink { get; set; }
        public Guid? SubId { get; set; }
        public ICollection<ScreenItemDTO> Item { get; set; }
    }
}
