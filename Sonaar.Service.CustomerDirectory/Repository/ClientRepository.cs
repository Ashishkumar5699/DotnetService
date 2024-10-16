using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Sonaar.Domain.DataContexts;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Entities.Contacts;

namespace Sonaar.Service.CustomerDirectory.Repository;

public class ClientRepository : IClientRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ClientRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public async Task<ConsumerDTO> AddContacts(ConsumerDTO consumerDTO)
    {
        var isExist = await _dataContext.ContactDetails.AnyAsync(x => x.ContactPhoneNumber == consumerDTO.ContactPhoneNumber);

        if (isExist)
        {
            throw new Exception("Phone number already Exist");
        }

        var _contactDetails = _mapper.Map<ContactDetails>(consumerDTO);

        var contact = await _dataContext.ContactDetails.AddAsync(_contactDetails);

        await _dataContext.SaveChangesAsync();

        return consumerDTO;
    }

    public async Task<IEnumerable<ContactDetails>> GetAllContacts()
    {
        var result = await _dataContext.ContactDetails.ToListAsync();
        return result;
    }

    public async Task<ContactDetails> GetContactswithPhone(string phone)
    {
        var isExist = await _dataContext.ContactDetails.AnyAsync(x => x.ContactPhoneNumber == phone.ToLower());
        if(isExist)
        {
            var result = await _dataContext.ContactDetails.SingleOrDefaultAsync(x => x.ContactPhoneNumber == phone.ToLower());
            return result;
        }

        throw new Exception("User is not Exist");
    }
}
