namespace QuickFix.Settings.Screens.Model
{
    public class ScreenModel
    {
        public Guid Id { get; set; }
        public string? HashName { get; set; }
        public int order { get; set; }
        public string Label { get; set; }
        public string? Icon { get; set; }
        public string? IconImge { get; set; }
        public string Translate { get; set; }
        public string RouterLink { get; set; }
        /*   public IEnumerable<RouterLinks> RouterLink { get; set; }*/
        public Guid? SubId { get; set; }
        public ICollection<UserScreen> Role { get; set; }
    }
    public class RouterLinks
    {
        public string RouterLink { get; set; }
    }
}
