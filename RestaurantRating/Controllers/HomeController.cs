using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantRating.KNN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestaurantRating.Data;
using RestaurantRating.Models;
using Microsoft.EntityFrameworkCore;

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

            var RestrauntsByReviews = _context.Review.Include(o => o.Restaurant).GroupBy(r => r.Restaurant.Name).Select(g => new { restaurant = g.Key.ToString(), count = g.Count() }).ToList();
            var RestrauntsByReviewsJSON = JsonConvert.SerializeObject(RestrauntsByReviews);
            ViewData["RestrauntsByReviewsJSON"] = RestrauntsByReviewsJSON;
            var connected_claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Sid);
            //ViewBag.mostPopResraunt = _context.Users.GroupBy(r => r.favoriteComputer).Select(g => new { Computer = ((Computer)g.Key), count = g.Count() }).OrderByDescending(r => r.count).FirstOrDefault()?.Computer;
            if (connected_claim != null)
            {
                var connected_user = _context.User.Find(Int32.Parse(connected_claim.Value));
                ViewBag.Suggestions = this.GetSuggestions(connected_user);
            }
            return View();
        }

        public List<Restaurant> GetSuggestions(User user)
        {
            int numberOfSuggestions = 3;
            // select all restraunts.
            List<Restaurant> userRestaurants = _context.Restaurant.ToList();

            
            if (numberOfSuggestions > userRestaurants.Count())
            {
                return userRestaurants;
            }
            KNNAlgorithm knn = new KNNAlgorithm(numberOfSuggestions, userRestaurants);
            return knn.GetNearestFor(user).OrderByDescending(r => r.Rating).ToList();

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
