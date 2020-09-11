using ApartmentSearch.Data;
using ApartmentSearch.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace ApartmentSearch.Service
{
    public class ListingService : IListing
    {
        private readonly ApartmentsDbContext _context;
        private readonly IWebHostEnvironment _env;
        private static readonly string[] _extentions = { ".jpg", ".jpeg", ".bmp", ".png" };

        public ListingService(ApartmentsDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IEnumerable<ApartmentListing> GetAll()
        {
            return _context.ApartmentListings;
        }

        public IEnumerable<ApartmentListing> GetUserApartment(string userId)
        {
            return GetAll()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.DateCreated);
        }

        public ApartmentListing GetById(int apartmentId)
        {
            return GetAll()
                .FirstOrDefault(x => x.Id == apartmentId);
        }

        public void AddListing(ApartmentListing newListing)
        {
            _context.Add(newListing);
            _context.SaveChangesAsync();
        }

        public void UpdateListing(ApartmentListing editListing)
        {
            _context.Update(editListing);
            _context.SaveChangesAsync();
        }

        public void DeleteListing(int apartmentId)
        {
            var listing = GetById(apartmentId);
            if(listing != null) 
            {
                _context.Remove(listing);
                _context.SaveChangesAsync();
            }
        }

        public bool IsImage(string filename)
        {
            string ext = GetImageExtension(filename);
            return _extentions.Contains(ext, StringComparer.OrdinalIgnoreCase);
        }

        public bool ListingExist(int apartmentId)
        {
            return _context.ApartmentListings.Any(x => x.Id == apartmentId);
        }

        public string GetListingImage(int apartmentId)
        {
            return GetApartmentImages(apartmentId).FirstOrDefault();
  
        }

        public string GetImageExtension(string filename)
        {
            return Path.GetExtension(filename);
        }

        public void DeleteListingPhotos(int apartmentId)
        {
            // Select all the filename of images associated with the listing
            var listingPhotos = GetApartmentImages(apartmentId);

            // List is not empty Delete all images from the directory
            if (listingPhotos != null)
            {
                foreach(var photo in listingPhotos)
                {
                    try
                    {
                        // Construct the relative path of the file
                        string filePath = $"{_env.WebRootPath}{photo}";
                        // Delete file from wwwroot folder
                        File.Delete(filePath);
                    }
                    catch(DirectoryNotFoundException dirNotFound)
                    {
                        Console.WriteLine(dirNotFound.Message);
                    }
                }
            }
        }

        public IEnumerable<string> GetApartmentImages(int apartmentId)
        {
            return _context.ListingImages
                .Where(x => x.ApartmentListing.Id == apartmentId)
                .Select(x => x.ImageUrl);
        }

        public void DeleteListingImage(int ImageId)
        {
            // Select filename of the image
            string filename = _context.ListingImages.FirstOrDefault(x => x.Id == ImageId).ImageUrl;

            try
            {
                // Construct the relative path of the file
                string filePath = $"{_env.WebRootPath}{filename}";
                File.Delete(filePath);
            }
            catch (DirectoryNotFoundException dirNotFound)
            {
                Console.WriteLine(dirNotFound.Message);
            }
        }

        public void UploadListingImage(string filename, string fileExtension, IFormFile imageFile)
        {
            // Create path for new file
            string filePath = $"{_env.WebRootPath}\\images\\listings\\{filename}{fileExtension}";
            using (FileStream stream = System.IO.File.Create(filePath))
            {
                imageFile.CopyTo(stream);
                stream.Flush();
            }
        }
    }
}
