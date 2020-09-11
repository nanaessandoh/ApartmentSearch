using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentSearch.Models.Home
{
    public class ListingDetailModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public double PricePerMonth { get; set; }
        public int NoOfBedrooms { get; set; }
        public int NoOfBaths { get; set; }
        public bool OffStreetParking { get; set; }
        public bool LaundryAvailable { get; set; }
        public DateTime DateCreated { get; set; }
        public string Cartegory { get; set; }
        public virtual IEnumerable<string> Images { get; set; }
    }
}
