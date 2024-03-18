using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VivesActivities.Model;
using VivesActivities.Sdk;
using VivesActivities.Services;

namespace VivesActivities.Ui.Mvc.Controllers
{
    //[Authorize]
    public class LocationsController(
        LocationService locationService,
        LocationSdk locationSdk) : Controller
    {
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var locations = await locationSdk.Find();
            return View(locations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            locationService.Create(location);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var location = locationService.Get(id);

            if (location is null)
            {
                return RedirectToAction("Index");
            }

            return View(location);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Location location)
        {
            if (!ModelState.IsValid)
            {
                return View(location);
            }

            locationService.Update(id, location);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var location = locationService.Get(id);

            if (location is null)
            {
                return RedirectToAction("Index");
            }

            return View(location);
        }

        [HttpPost("[controller]/delete/{id:int?}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            locationService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
