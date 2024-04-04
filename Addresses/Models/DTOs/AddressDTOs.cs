namespace QuickFix.Addresses.Models.DTOs
{
    public class AddressDTOs
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string description { get; set; }
    }
}
