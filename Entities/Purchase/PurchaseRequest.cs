using System.ComponentModel.DataAnnotations;

namespace APIServices.Entities.Purchase
{
    public class PurchaseRequest
    {
        [Key]
        public int PurchaseRequestId { get; set; }
        public string MatelType { get; set; } = string.Empty;
        public string ItemType { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string ManufactureId { get; set; } = string.Empty;
        public string ManufactureName { get; set; } = string.Empty;
        public double GrossWeight { get; set; }
        public double LessWeight { get; set; }
        public double NetWeight { get; set; }
        public int Quantity { get; set; } = 0;
        public double Rate { get; set; }
        public double Wastage { get; set; }
        public double Labour { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int? IsApproved { get; set; }
    }
}
