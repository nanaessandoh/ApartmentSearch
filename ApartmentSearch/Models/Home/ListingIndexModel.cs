using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentSearch.Models.Home
{
    public class ListingIndexModel
    {
          public IEnumerable<ListingModel> Assets { get; set; }
    }

    public class ListingModel
    {
        public int ListingId { get; set; }
        public string Description { get; set; }
        public int NoOfBedrooms { get; set; }
        public int NoOfBaths { get; set; }
        public bool OffStreetParking { get; set; }
        public bool LaundryAvailable { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public double PricePerMonth { get; set; }
    }
}
