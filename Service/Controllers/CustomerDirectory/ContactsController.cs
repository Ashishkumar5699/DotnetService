using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Controllers;
using Sonaar.Domain.DataContext;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Entities.Contacts;
using Sonaar.Domain.Response;
using Sonaar.Interface;

namespace Sonaar.Service.APi.Controllers.CustomerDirectory
{
    public class ContactsController : BaseApiController
    {
        private readonly IMapper _mapper;
        public ContactsController(DataContext context, ITokenService tokenService, IMapper mapper) : base(context, tokenService)
        {
            _mapper = mapper;
        }

        [HttpPost("AddContacts")]
        public async Task<ActionResult<ResponseResult<ConsumerDTO>>> AddContacts(ConsumerDTO consumerDTO)
        {
            var result = new ResponseResult<ConsumerDTO>();

            try
            {
                var isExist = await _context.ContactDetails.AnyAsync(x => x.ContactPhoneNumber == consumerDTO.ContactPhoneNumber);

                if (isExist)
                {
                    result.Message = "Phone number already Exist";

                    return result;
                }

                var _contactDetails = _mapper.Map<ContactDetails>(consumerDTO);

                var contact = await _context.ContactDetails.AddAsync(_contactDetails);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                result.HasErrors = true;
                result.Message = $"Error {ex}";
            }

            return result;
        }

        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<ResponseResult<IEnumerable<ContactDetails>>>> GetAllContacts()
        {
            var response = new ResponseResult<IEnumerable<ContactDetails>>();
            try
            {
                var result = await _context.ContactDetails.ToListAsync();
                response.Data = result;
            }
            catch (Exception ex)
            {
                response.HasErrors = true;
                response.Message = ex.Message;
                //BadRequest(ex?.Message);
            }
            return response;
        }

        [HttpPost("GetContactswithPhone")]
        public async Task<ActionResult<ResponseResult<ContactDetails>>> GetContactswithPhone(string phone)
        {
            var response = new ResponseResult<ContactDetails>();
            try
            {
                var isExist = await _context.ContactDetails.AnyAsync(x => x.ContactPhoneNumber == phone.ToLower());
                if(isExist)
                {
                    var result = await _context.ContactDetails.SingleOrDefaultAsync(x => x.ContactPhoneNumber == phone.ToLower());
                    response.Data =  result;
                }
            }
            catch (Exception ex)
            {
                response.HasErrors = true;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

