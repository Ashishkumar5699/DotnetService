using System;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Entities.Contacts;

namespace Sonaar.Service.CustomerDirectory.Repository;

public interface IClientRepository
{
    Task<ConsumerDTO> AddContacts(ConsumerDTO consumerDTO);
    
    Task<IEnumerable<ContactDetails>> GetAllContacts();
    
    Task<ContactDetails> GetContactswithPhone(string phone);

}