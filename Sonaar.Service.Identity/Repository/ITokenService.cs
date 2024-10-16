using System;
using Sonaar.Domain.Entities.Authentication;

namespace Sonaar.Service.Identity.Repository;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
