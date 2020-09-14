using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApartmentSearch.Data;
using ApartmentSearch.Models.Home;
using ApartmentSearch.Service.Interface;
using Microsoft.AspNetCore.Authorization;
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
            var currentUserId = _userManager.GetUserId(User);
            var allListings = _listingService.GetUserApartment(currentUserId);

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
        public async Task<ActionResult> Create([Bind("Id, Description, Address, PostCode, PricePerMonth, NoOfBedrooms, NoOfBaths, OffStreetParking, LaundryAvailable, Files")] ApartmentListing apartmentListing)
        {
            // Check if the model is Valid
            if (ModelState.IsValid)
            {
                // Check if Files were selected for the Listing
                if(apartmentListing.Files != null)
                {
                    // Check if files are all images i.e. jpeg, jpg, png, bmp
                    if(_listingService.AllFilesAreImages(apartmentListing.Files))
                    {
                        // Create an empty IEnumerable of Listing Images
                        IEnumerable<ListingImage> Images = Enumerable.Empty<ListingImage>();

                        foreach ( var formFile in apartmentListing.Files)
                        {
                            // Create a random file name for the Profile Image
                            string randomFileName = Guid.NewGuid().ToString();
                            // Get the extension of the filename
                            string imageExtension = _listingService.GetImageExtension($"{formFile.FileName}");
                            // Create the URL to the image
                            string imageUrl = $"/images/listings/{randomFileName}{imageExtension}";
                            // Create an Image listing object
                            var image = new ListingImage
                            {
                                ApartmentListing = apartmentListing,
                                ImageUrl = imageUrl
                            };
                            // Add the Image to IEnumerator of Listing Images
                            Images.Append(image);
                            // Copy the Image into the wwwroot/images/listings folder
                            _listingService.UploadListingImage(randomFileName, imageExtension, formFile);
                        }

                        // Assign Images 
                        apartmentListing.Images = Images;
                        // Get and Assign User
                        var currentUser = await _userManager.GetUserAsync(User);
                        apartmentListing.User = currentUser ;
                        // Get and Assign Current time
                        var currentTime = DateTime.Now;
                        apartmentListing.DateCreated = currentTime;
                        // Get the Category

                        // Save the listing
                        _listingService.AddListing(apartmentListing);

                    }
                    // Alert Upload Images
                    TempData["Message"] = "Accepted Extensions are .jpeg .jpg .png .bmp";
                    return View(apartmentListing);


                }
                // Alert Upload Images
                TempData["Message"] = "Select Photo(s) for your Listing ";
                return View(apartmentListing);

            }
            return View(apartmentListing);

        }

        // GET: Listing/Edit/5
        public ActionResult Edit(int id)
        {
            var listing = _listingService.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        // POST: Listing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, [Bind("Id, Description, Address, PostCode, PricePerMonth, NoOfBedrooms, NoOfBaths, OffStreetParking, LaundryAvailable, Files")] ApartmentListing apartmentListing)
        {
            if( id != apartmentListing.Id)
            {
                return NotFound();
            }

            // Check if the model is Valid
            if (ModelState.IsValid)
            {
                // Add More Images to the listing
                // Check if Files were selected for the Listing
                if (apartmentListing.Files != null)
                {
                    // Check if files are all images i.e. jpeg, jpg, png, bmp
                    if (_listingService.AllFilesAreImages(apartmentListing.Files))
                    {

                        foreach (var formFile in apartmentListing.Files)
                        {
                            // Create a random file name for the Profile Image
                            string randomFileName = Guid.NewGuid().ToString();
                            // Get the extension of the filename
                            string imageExtension = _listingService.GetImageExtension($"{formFile.FileName}");
                            // Create the URL to the image
                            string imageUrl = $"/images/listings/{randomFileName}{imageExtension}";
                            // Create an Image listing object
                            var image = new ListingImage
                            {
                                ApartmentListing = apartmentListing,
                                ImageUrl = imageUrl
                            };
                            // Add the Image to the Listing's Images
                            apartmentListing.Images.Append(image);
                            // Copy the Image into the wwwroot/images/listings folder
                            _listingService.UploadListingImage(randomFileName, imageExtension, formFile);
                        }

                        // Update the listing
                        _listingService.UpdateListing(apartmentListing);
                        // Alert that Listing has been Updated
                        TempData["Success"] = "Listing Updated Successfully";
                        return RedirectToAction("Edit", new { Id = id });

                    }
                    // Alert Upload Images
                    TempData["Message"] = "Accepted Extensions are .jpeg .jpg .png .bmp";
                    return View(apartmentListing);

                }

                if (apartmentListing.Files == null)
                {
                    _listingService.UpdateListing(apartmentListing);
                }

            }

            return View(apartmentListing);
        }

        // GET: Listing/Delete/5
        public ActionResult Delete(int id)
        {
            var listing = _listingService.GetById(id);
            if (listing == null)
            {
                return NotFound();
            }
            return View(listing);
        }

        // POST: Listing/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _listingService.DeleteListing(id);
            _listingService.DeleteListingPhotos(id);
            return RedirectToAction(nameof(Index));
        }
    }
}