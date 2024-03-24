using APIServices.Entities;

namespace APIServices.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
