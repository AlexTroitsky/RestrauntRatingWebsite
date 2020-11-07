using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantRating.Data;

namespace RestaurantRating.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeController
        private readonly RestaurantRatingContext _context;
        public HomeController(RestaurantRatingContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var RestrauntsByCity = _context.Restaurant.GroupBy(r => r.City).Select(g => new { city = g.Key.ToString(), count = g.Count() }).ToList();
            var RestrauntsByCityJSON = JsonConvert.SerializeObject(RestrauntsByCity);
            ViewData["RestrauntsByCityJSON"] = RestrauntsByCityJSON;

            var RestrauntsByReviews = _context.Restaurant.GroupBy(r => r.Id).Select(g => new { review = g.Key.ToString(), count = g.Count() }).ToList();
            var RestrauntsByReviewsJSON = JsonConvert.SerializeObject(RestrauntsByReviews);
            ViewData["RestrauntsByReviewsJSON"] = RestrauntsByReviewsJSON;

            //ViewBag.mostPopResraunt = _context.Users.GroupBy(r => r.favoriteComputer).Select(g => new { Computer = ((Computer)g.Key), count = g.Count() }).OrderByDescending(r => r.count).FirstOrDefault()?.Computer;


            return View();
        }

        public ActionResult About()
        {
            return View();
        }


        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
