﻿using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Sonaar.Controllers;
using Sonaar.Domain.Constants;
using Sonaar.Domain.Dto.Authentication;
using Sonaar.Domain.ResponseObject;
using Sonaar.Interface;
using Sonaar.Domain.Entities.Authentication;
using Sonaar.Domain.DataContexts;
using Sonaar.Service.Identity.Service;
using Sonaar.Domain.Response;

namespace Sonaar.Service.APi.Controllers.IdentityController
{
    public class AccountController : BaseApiController
    {
        private readonly IdentityService _identityService;
        #region Constructor
        public AccountController(DataContext context, ITokenService tokenService,IdentityService identityService) : base(context, tokenService)
        {
            _identityService = identityService;
        }
        #endregion

        #region APIs
        [HttpPost("register")]
        public async Task<ResponseResult<AuthUserResponse>> Register(RegisterDto registerDto)
        {
            var result = new ResponseResult<AuthUserResponse>();

            var data = new AuthUserResponse
            {
                UserName = registerDto.UserName,
                Password = registerDto.Password
            };

            result = await _identityService.Register(data);

            return result;

            // if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");

            // using var hmac = new HMACSHA512();

            // var user = new AppUser
            // {
            //     UserName = registerDto.UserName.ToLower(),
            //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            //     PasswordSalt = hmac.Key
            // };

            // _context.Users.Add(user);

            // await _context.SaveChangesAsync();

            // return new AuthUserResponse
            // {
            //     UserName = registerDto.UserName,
            //     Password = null,
            //     Token = _tokenService.CreateToken(user)
            // };
        }

        [HttpPost("login")]
        public async Task<ActionResult<Sonaar.Domain.Response.ResponseResult<AuthUserResponse>>> Login(LoginDto loginDto)
        {
            var result = new ResponseResult<AuthUserResponse>();

            var data = new AuthUserResponse
            {
                UserName = loginDto.UserName,
                Password = loginDto.Password
            };

            result = await _identityService.Login(data);

            return result;
            // var user = new AppUser();
            // try
            // {
            //     user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            // }
            // catch (Exception ex)
            // {
            //     return new Sonaar.Domain.Response.ResponseResult<AuthUserResponse>
            //     {
            //         HasErrors = true,
            //         Message = ex.ToString(),//GlobalMessages.InvalidUsername
            //     };
            // }

            // if (user == null)
            //     return new Domain.Response.ResponseResult<AuthUserResponse>
            //     {
            //         HasErrors = true,
            //         Message = GlobalMessages.InvalidUsername
            //     };

            // using var hmac = new HMACSHA512(user.PasswordSalt);

            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            // for (int i = 0; i < computedHash.Length; i++)
            // {
            //     if (computedHash[i] != user.PasswordHash[i])
            //         return new Domain.Response.ResponseResult<AuthUserResponse>
            //         {
            //             HasErrors = true,
            //             Message = GlobalMessages.InvalidPassword
            //         };
            // }

            // return new Domain.Response.ResponseResult<AuthUserResponse>
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
        
        }

        #endregion

        #region Methods
        private async Task<bool> UserExist(string username)
        {
            try
            {
                var result = await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion
    }
}

