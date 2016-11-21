using Microsoft.AspNetCore.Mvc;
using ADL.Models;
using System.Linq;
using System;

namespace ADL.Controllers
{
    public class LocationController : Controller
    {
        ILocationRepository repository;
        public LocationController(ILocationRepository repo)
        {
            repository = repo;
        }

        public ViewResult List() => View(repository.Locations);

        public ViewResult Edit(int locationId) =>
            View(repository.Locations
                .FirstOrDefault(l => l.LocationId == locationId));

        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid) {
                repository.SaveLocation(location);
                TempData["message"] = $"Lokationen '{location.Title}' blev gemt.";
                return RedirectToAction(nameof(List));
            } else {
                // there is something wrong with the data values
                return View(location);
            }
        }

        // Uses edit view, but gives it a new location as input
        public ViewResult Create() => View(nameof(Edit), new Location());

        [HttpPost]
        public IActionResult Delete(int locationId) {
            Location deletedLocation = repository.DeleteLocation(locationId);
            if (deletedLocation != null) {
                TempData["message"] = $"Lokationen '{deletedLocation.Title}' blev slettet.";
            }
            return RedirectToAction(nameof(List));
        }

        public ViewResult CreateQR(Location location)
        {
            QrGenerator.GenerateQR(location.LocationId);
            return View("ViewQR", location);
        }
    }
}