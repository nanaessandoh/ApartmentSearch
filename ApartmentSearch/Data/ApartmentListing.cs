using ApartmentSearch.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApartmentSearch.Data
{
    public class ApartmentListing
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name ="Description")]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Post Code")]
        public string PostCode { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Price Per Month")]
        public double PricePerMonth { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Range(1,20)]
        [Display(Name = "Number of Bedrooms")]
        public int NoOfBedrooms { get; set; }
        [Required]
        [Range(1, 20)]
        [DataType(DataType.Text)]
        [Display(Name = "Numbers of Baths")]
        public int NoOfBaths { get; set; }
        [Required]
        [Display(Name = "Off-Street Parking")]
        public bool OffStreetParking { get; set; }
        [Required]
        [Display(Name = "Laundry in Building")]
        public bool LaundryAvailable { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Apartment Type")]
        public virtual Category Category { get; set; }
        [DataType(DataType.ImageUrl)]
        public virtual IEnumerable<ListingImage> Images { get; set; }
        [NotMapped]
        [DataType(DataType.Upload)]
        [Display(Name = "Photos of Listing")]
        public IEnumerable<IFormFile> Files { get; set; }
        public virtual ApartmentsUser User { get; set; }

    }
}