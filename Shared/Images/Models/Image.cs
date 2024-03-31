using QuickFix.CategoriesItem.Models;

namespace QuickFix.Shared.Images.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid HadImage { get; set; }
        public string Url { get; set; }
    }
}
