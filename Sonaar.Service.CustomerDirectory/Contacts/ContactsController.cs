using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Controllers;
using Sonaar.Data;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Response;
using Sonaar.Entities.Contacts;
using Sonaar.Interface;

namespace Sonaar.Service.CustomerDirectory.Contacts
{
    public class ContactsController : BaseApiController
    {
        public ContactsController(DataContext context, ITokenService tokenService) : base(context, tokenService)
        {
        }

        [HttpPost("AddContacts")]
        public async Task<ActionResult<ResponseResult<ConsumerDTO>>> AddContacts(ConsumerDTO consumerDTO)
        {
            var result = new ResponseResult<ConsumerDTO>();

            try
            {
                var isExist = await _context.ContactDetails.AnyAsync(x => x.ContactPhoneNumber == consumerDTO.ContactPhoneNumber);

                if(isExist)
                {
                    result.HasErrors = true;

                    result.Message = "Phone number already Exist";

                    return result;
                }

                var _contactDetails = (ContactDetails)consumerDTO;

                var contact = await _context.ContactDetails.AddAsync(_contactDetails);

                await _context.SaveChangesAsync();

                result.Message = "Sucess";
            }
            catch (Exception ex)
            {
                result.Message = $"Error {ex}";
            }

            return result;
        }

        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<IEnumerable<ContactDetails>>> GetAllContacts()
        {
            try
            {
                var result = await _context.ContactDetails.ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                BadRequest(ex?.Message);
            }
            return null;
        }
    }
}

