using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApartmentSearch.Controllers
{
    public class ListingController : Controller
    {
        // GET: ListingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ListingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
