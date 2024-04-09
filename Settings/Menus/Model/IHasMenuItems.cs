namespace QuickFix.Settings.Menus.Model
{
    public interface IHasMenuItems
    {

        public Guid? Id { get; set; }
        public string? Label { get; set; }
        public string? Icon { get; set; }
        public string? IconImge { get; set; }
        public string? Translate { get; set; }
        public string? HashName { get; set; }
        public IEnumerable<string>? RouterLink { get; set; }
        public Guid? SubId { get; set; }
        public List<ApplicationMenuItem> Items { get; set; }

    }
}
