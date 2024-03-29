using APIServices.Data;
using APIServices.DTOs;
using APIServices.Entities;
using APIServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PunjabOrnaments.Common.Constants;
using System.Security.Cryptography;
using System.Text;

namespace APIServices.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        public AccountController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }
        #region APIs
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExist(registerDto.UserName)) return BadRequest("Username is taken");
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.UserName.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return new UserDto
            {
                UserName = registerDto.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<PunjabOrnaments.Common.Models.Response.ResponseResult<UserDto>>> Login(LoginDto loginDto)
        {
            var user = new AppUser();
            try
            {
                user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);
            }
            catch (Exception ex)
            {
                return new PunjabOrnaments.Common.Models.Response.ResponseResult<UserDto>
                {
                    HasErrors = true,
                    Message = ex.ToString(),//GlobalMessages.InvalidUsername
                };
            }


            if (user == null)
                return new PunjabOrnaments.Common.Models.Response.ResponseResult<UserDto>
                {
                    HasErrors = true,
                    Message = GlobalMessages.InvalidUsername
                };

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return new PunjabOrnaments.Common.Models.Response.ResponseResult<UserDto>
                    {
                        HasErrors = true,
                        Message = GlobalMessages.InvalidPassword
                    };
            }

            return new PunjabOrnaments.Common.Models.Response.ResponseResult<UserDto>
            {
                Message = GlobalMessages.SucessMessage,
                Data = new UserDto()
                {
                    UserName = loginDto.UserName,
                    Token = _tokenService.CreateToken(user),
                    LoginTime = DateTime.Now,
                    //Device = loginDto.Device,
                }
            };
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
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}
