using System;
using QuickFix.Identity.Shared.Models;
using QuickFix.Settings.Menus.Model;
using QuickFix.Settings.Screens.Model.DTOs;

namespace QuickFix.Identity.Identitys.Features.Login.v1
{
    public record LoginResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public LoginData Data { get; set; }
    }
    public class LoginData
    {
        public Guid Id { get; set; }
        public string? AccessToken { get; set; }
        public string? FirstName { get; set; }
        public string? Username { get; set; }
        public string? RefreshToken { get; set; }
        public List<ApplicationMenuItem> menu { get; set; }
        public IEnumerable<UserScreenDTO> Permissions { get; set; }
    }
}
