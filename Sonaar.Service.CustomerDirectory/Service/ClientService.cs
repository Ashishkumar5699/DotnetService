using System;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Entities.Contacts;
using Sonaar.Domain.Response;
using Sonaar.Service.CustomerDirectory.Repository;

namespace Sonaar.Service.CustomerDirectory.Service;

public class ClientService
{
    private readonly IClientRepository _clientRepository;
    public ClientService(IClientRepository clientRepository)
    {
        _clientRepository = clientRepository;
    }

    public async Task<ResponseResult<ConsumerDTO>> AddContacts(ConsumerDTO consumerDTO)
    {
        var result = new ResponseResult<ConsumerDTO>();

        try
        {
            result.Data = await _clientRepository.AddContacts(consumerDTO);
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.HasErrors = true;
        }

        return result;
    }

    public async Task<ResponseResult<IEnumerable<ContactDetails>>> GetAllContacts()
    {
        var result = new ResponseResult<IEnumerable<ContactDetails>>();
        try
        {
            result.Data = await _clientRepository.GetAllContacts();
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.HasErrors = true;
        }
        return result;
    }
    public async Task<ResponseResult<ContactDetails>> GetContactswithPhone(string phone)
    {
        var result = new ResponseResult<ContactDetails>();
        try
        {
            result.Data = await _clientRepository.GetContactswithPhone(phone);
        }
        catch (System.Exception ex)
        {
            result.Message = ex.Message;
            result.HasErrors = true;
        }
        return result;
    }

}
