namespace Sonaar.DTOs
{
    public class UserDto : Sonaar.Domain.Models.Auth.AuthUser
    {
        //public string UserName { get; set; }
        //public string Token { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
