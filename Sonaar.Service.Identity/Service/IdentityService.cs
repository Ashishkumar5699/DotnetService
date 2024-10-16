using System;
using Sonaar.Domain.Dto.Authentication;
using Sonaar.Domain.Response;
using Sonaar.Domain.ResponseObject;
using Sonaar.Service.Identity.Repository;

namespace Sonaar.Service.Identity.Service;

public class IdentityService
{
    private readonly IIdentityRepository _identityRepository;

    public IdentityService(IIdentityRepository identityRepository)
    {
        _identityRepository = identityRepository;
    }

    public async Task<ResponseResult<AuthUserResponse>> Register(AuthUserResponse response)
    {
        var result = new ResponseResult<AuthUserResponse>();
        try
        {
            result.Data = await _identityRepository.Register(response);
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.Data = response;
            result.HasErrors = true;
        }

        result.Data.Password = null;

        return result;
    }

    public async Task<ResponseResult<AuthUserResponse>> Login(LoginDto loginDto)
    {
        var result = new ResponseResult<AuthUserResponse>();

        try
        {
            result.Data = await _identityRepository.Login(loginDto);
            result.Data.Password = null;
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.HasErrors = true;
        }


        return result;
    }

}
