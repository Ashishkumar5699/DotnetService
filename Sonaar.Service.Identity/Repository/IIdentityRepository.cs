using System;
using Sonaar.Domain.Dto.Authentication;
using Sonaar.Domain.ResponseObject;

namespace Sonaar.Service.Identity.Repository;

public interface IIdentityRepository
{
    Task<AuthUserResponse> Register(AuthUserResponse response);
    
    Task<AuthUserResponse> Login(LoginDto loginDto);

}
