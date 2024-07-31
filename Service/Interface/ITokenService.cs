using Sonaar.Entities;

namespace Sonaar.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
