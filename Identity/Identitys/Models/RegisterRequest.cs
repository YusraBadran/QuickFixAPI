namespace QuickFix.Identitys.Models
{
    public record class RegisterRequest
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string phoneNumber { get; set; }
        public string password { get; set; }
        public string confegerPassword { get; set; }
        public List<string> Roles { get; set; }
    }   
}