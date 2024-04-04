namespace QuickFix.Addresses.Features.CreateAddresses.v1
{
    public record CreateAddresRequest
    {
        public string? Location { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public string? Description { get; set; }
    }
}
