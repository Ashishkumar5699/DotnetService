using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sonaar.Controllers;
using Sonaar.Domain.DataContexts;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Entities.Contacts;
using Sonaar.Domain.Response;
using Sonaar.Interface;
using Sonaar.Service.CustomerDirectory.Service;

namespace Sonaar.Service.APi.Controllers.CustomerDirectory
{
    public class ContactsController : BaseApiController
    {
        private readonly IMapper _mapper;
        private readonly ClientService _clientService;
        public ContactsController(DataContext context, ITokenService tokenService, IMapper mapper,ClientService clientService) : base(context, tokenService)
        {
            _mapper = mapper;
            _clientService = clientService;
        }

        [HttpPost("AddContacts")]
        public async Task<ActionResult<ResponseResult<ConsumerDTO>>> AddContacts(ConsumerDTO consumerDTO)
        {
            return await _clientService.AddContacts(consumerDTO);
        }

        [HttpGet("GetAllContacts")]
        public async Task<ActionResult<ResponseResult<IEnumerable<ContactDetails>>>> GetAllContacts()
        {
            return await _clientService.GetAllContacts();
        }

        [HttpPost("GetContactswithPhone")]
        public async Task<ActionResult<ResponseResult<ContactDetails>>> GetContactswithPhone(string phone)
        {
            return await _clientService.GetContactswithPhone(phone);
        }
    }
}

