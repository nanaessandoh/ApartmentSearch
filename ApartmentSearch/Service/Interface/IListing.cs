using ApartmentSearch.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentSearch.Service.Interface
{
    public interface IListing
    {
        IEnumerable<ApartmentListing> GetAll();
        IEnumerable<ApartmentListing> GetUserApartment(string userId);
        ApartmentListing GetById(int apartmentId);
        void AddListing(ApartmentListing newListing);
        void UpdateListing(ApartmentListing editListing);
        void DeleteListing(int apartmentId);
        bool IsImage(string filename);
        bool ListingExist(int apartmentId);
        string GetListingImage(int apartmentId);
        string GetImageExtension(string filename);
        void DeleteListingPhotos(int apartmentId);
        IEnumerable<string> GetApartmentImages(int apartmentId);
        void UploadListingImage(string filename, string fileExtension, IFormFile imageFile);
        void DeleteListingImage(int ImageId);

    }
}
