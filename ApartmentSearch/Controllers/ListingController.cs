using System.Linq;
using ApartmentSearch.Data;
using ApartmentSearch.Models.Home;
using ApartmentSearch.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentSearch.Controllers
{
    public class ListingController : Controller
    {
        private readonly IListing _listingService;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApartmentsUser> _userManager;

        public ListingController(IListing listingService, IWebHostEnvironment env, UserManager<ApartmentsUser> userManager)
        {
            _listingService = listingService;
            _env = env;
            _userManager = userManager;

        }

        // GET: ListingController
        public ActionResult Index()
        {
            var allListings = _listingService.GetAll();

            var listingResult = allListings.Select(x => new ListingModel
            {
                ListingId = x.Id,
                Category = x.Cartegory.Name,
                Description = x.Description,
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
                Cartegory = listingModel.Cartegory.Name,
                DateCreated = listingModel.DateCreated,
                Description = listingModel.Description,
                Images = _listingService.GetApartmentImages(id),
                LaundryAvailable = listingModel.LaundryAvailable,
                NoOfBaths = listingModel.NoOfBaths,
                NoOfBedrooms = listingModel.NoOfBedrooms,
                OffStreetParking = listingModel.OffStreetParking,
                PostCode = listingModel.PostCode,
                PricePerMonth = listingModel.PricePerMonth
            };

            return View(model);
        }

        // GET: ListingController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ListingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListingController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ListingController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ListingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ListingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}