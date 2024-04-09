namespace QuickFix.Settings.Screens.Model.DTOs
{
    public class UserScreenDTO
    {
        public Guid Id { get; set; }
        public string HashName { get; set; }
        public string Translate { get; set; }
        public bool Menu { get; set; } = false;
        public bool IsView { get; set; } = false;
        public bool IsDetail { get; set; } = false;
        public bool IsCreated { get; set; } = false;
        public bool IsUpdated { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
        public bool IsPrint { get; set; } = false;
        public bool IsExport { get; set; } = false;
        public bool IsImport { get; set; } = false;
    }
}
