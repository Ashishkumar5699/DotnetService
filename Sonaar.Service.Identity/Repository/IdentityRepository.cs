using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.Constants;
using Sonaar.Domain.DataContexts;
using Sonaar.Domain.Dto.Authentication;
using Sonaar.Domain.Entities.Authentication;
using Sonaar.Domain.ResponseObject;

namespace Sonaar.Service.Identity.Repository;

public class IdentityRepository : IIdentityRepository
{
    private readonly DataContext _dataContext;
    private readonly ITokenService _tokenService;

    public IdentityRepository(DataContext dataContext,ITokenService tokenService)
    {
        _dataContext = dataContext;
        _tokenService = tokenService;
    }

    public async Task<AuthUserResponse> Register(AuthUserResponse response)
    {
        if (await UserExist(response.UserName))
        {
            throw new Exception("Username is taken");
        }

        using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = response.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(response.Password)),
                PasswordSalt = hmac.Key
            };

            _dataContext.Users.Add(user);

            await _dataContext.SaveChangesAsync();

            response.Password = null;
            response.Token = _tokenService.CreateToken(user);


        return response;

    }

    public async Task<AuthUserResponse> Login(LoginDto loginDto)
    {
        var result = new AuthUserResponse();
        var user = new AppUser();
        try
        {
            user = await _dataContext.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
        }
        catch (Exception)
        {
            throw;
        }

        if (user == null)
        {
            throw new Exception(GlobalMessages.InvalidUsername);
        }

        // return new Domain.Response.ResponseResult<AuthUserResponse>
        // {
        //     HasErrors = true,
        //     Message = GlobalMessages.InvalidUsername
        // };

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
            {
                throw new Exception(GlobalMessages.InvalidPassword);
            }

            // return new Domain.Response.ResponseResult<AuthUserResponse>
            // {
            //     HasErrors = true,
            //     Message = GlobalMessages.InvalidPassword
            // };
        }

        result.UserName = loginDto.UserName;
        result.LoginTime = DateTime.Now;
        result.Device = loginDto.Device;
        result.Token = _tokenService.CreateToken(user);


            // return new AuthUserResponse
            // {
            //     Message = GlobalMessages.SucessMessage,
            //     Data = new AuthUserResponse()
            //     {
            //         UserName = loginDto.UserName,
            //         Password = null,
            //         Token = _tokenService.CreateToken(user),
            //         LoginTime = DateTime.Now,
            //         //Device = loginDto.Device,
            //     }
            // };
        return result;
    }

    private async Task<bool> UserExist(string username)
    {
        try
        {
            var result = await _dataContext.Users.AnyAsync(x => x.UserName == username.ToLower());
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
