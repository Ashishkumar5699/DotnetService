namespace APIServices.DTOs
{
    public class UserDto : PunjabOrnaments.Common.Models.Auth.AuthUser
    {
        //public string UserName { get; set; }
        //public string Token { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
