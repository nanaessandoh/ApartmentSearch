using ApartmentSearch.Data;
using System.ComponentModel.DataAnnotations;

namespace ApartmentSearch.Data
{
    public class ListingImage
    {
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public virtual ApartmentListing ApartmentListing { get; set; }
    }
}
