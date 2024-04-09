using QuickFix.Identity.Shared.Models;

namespace QuickFix.Settings.Screens.Model
{
    public class UserScreen
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ScreenId { get; set; }
        public virtual ScreenModel Screen { get; set; }
        public virtual ApplicationUser? User { get; set; }
        public bool? Menu { get; set; } = false;
        public bool? IsView { get; set; } = false;
        public bool? IsDetail { get; set; } = false;
        public bool? IsCreated { get; set; } = false;
        public bool? IsUpdated { get; set; } = false;
        public bool? IsDeleted { get; set; } = false;
        public bool? IsPrint { get; set; } = false;
        public bool? IsExport { get; set; } = false;
        public bool? IsImport { get; set; } = false;
    }
}
