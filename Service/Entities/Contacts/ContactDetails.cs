using System.ComponentModel.DataAnnotations;
using Sonaar.Domain.Dto.CustomerDirectory;
using Sonaar.Domain.Enum;

namespace Sonaar.Entities.Contacts
{
    public class ContactDetails : ConsumerDTO
    {
        [Key]
        public int ContactId { get; set; }

    }
}
