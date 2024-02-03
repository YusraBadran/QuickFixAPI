using AutoMapper;

namespace QuickFix.Categories.Models.DTOs
{
    public record CategoryDTOs
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryState State { get; set; }
        public Guid ServiceId { get; set; }
    }
}
