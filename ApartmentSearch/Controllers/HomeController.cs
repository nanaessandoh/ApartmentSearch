using System.Linq;
using ApartmentSearch.Data;
using ApartmentSearch.Models.Home;
using ApartmentSearch.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IListing _listingService;

        public HomeController(IListing listingService) => _listingService = listingService;
        
        // GET: ListingController
        public ActionResult Index()
        {
            var allListings = _listingService.GetAll();

            var listingResult = allListings.Select(x => new ListingModel
            {
                ListingId = x.Id,
                Category = x.Category.Name,
                Description = x.Description,
                DatePosted = _listingService.ConvertToTimeAgo(x.DateCreated),
                NoOfBaths = x.NoOfBaths,
                NoOfBedrooms = x.NoOfBedrooms,
                OffStreetParking = x.OffStreetParking,
                LaundryAvailable = x.LaundryAvailable,
                PricePerMonth = x.PricePerMonth,
                ImageUrl = _listingService.GetListingImage(x.Id)
            });

            var model = new ListingIndexModel
            {
                Assets = listingResult
            };

            return View(model);
        }

        // GET: ListingController/Details/5
        public ActionResult Details(int id)
        {
            var listingModel = _listingService.GetById(id);

            var model = new ListingDetailModel
            {
                Id = listingModel.Id,
                Address = listingModel.Address,
                Category = listingModel.Category.Name,
                DateCreated = _listingService.ConvertToTimeAgo(listingModel.DateCreated),
                Description = listingModel.Description,
                Images = _listingService.GetApartmentImages(id),
                LaundryAvailable = listingModel.LaundryAvailable,
                NoOfBaths = listingModel.NoOfBaths,
                NoOfBedrooms = listingModel.NoOfBedrooms,
                OffStreetParking = listingModel.OffStreetParking,
                PostCode = listingModel.PostCode,
                PricePerMonth = listingModel.PricePerMonth,
                PosterFirstName = listingModel.User.FirstName
            };

            return View(model);
        }

    }
}
