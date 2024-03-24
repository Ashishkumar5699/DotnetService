using APIServices.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace APIServices.Entities.Contacts
{
    public class ContactDetails
    {
        [Key]
        public int ContactId { get; set; }
        public string? ContactPrifix { get; set; }
        public string? ContactFirstName { get; set; }
        public string? ContactLastName { get; set; }
        public string? ContactPhoneNumber { get; set; }
        public string? ContactAddress1 { get; set; }
        public string? ContactAddress2 { get; set; }
        public string? ContactLandMark { get; set; }
        public string? ContactCity { get; set; }
        public string? ContactState { get; set; }
        public string? ContactPinCode { get; set; }
        public ContactType ContactType { get;set; }
    }
}
