using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RestaurantRating.Data;
using RestaurantRating.Models;
using System.Security.Claims;

namespace RestaurantRating.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly RestaurantRatingContext _context;

        public ReviewsController(RestaurantRatingContext context)
        {
            _context = context;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _context.Review.Include(o => o.Restaurant).ToListAsync();
            return View(reviews);
        }

        // GET: ReviewsOfRestaurant
        public async Task<IActionResult> ReviewsOfRestaurant(int id)
        {
            var reviews = await _context.Review
                .Where(r => r.RestaurantId == id)
                .ToListAsync();
                return Json(reviews);
        }

        //Search
        public async Task<IActionResult> Search(string RestaurantName)
        {
            var reviews = await _context.Review.Where(r => r.Restaurant.Name.Contains(RestaurantName)).Include(o => o.Restaurant).ToListAsync();
            return View("Index", reviews);
        }

        // GET: Reviews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public IActionResult Create(int id = -1)
        {
            Restaurant restaurant = _context.Restaurant.Find(id);
            ViewBag.restaurant = restaurant;
            Review review = new Review();
            review.Restaurant = restaurant;
            return View(review);
        }


        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Stars,Price,UserId,RestaurantId")] Review review)
        {
            if (ModelState.IsValid)
            {
                var restaurant = _context.Restaurant.First(r => r.Id == review.RestaurantId);
                review.Date = DateTime.Now;
                _context.Add(review);
                List<Review> reviews = _context.Review.Where(dBreview => dBreview.RestaurantId == review.RestaurantId).ToList();
                reviews.Add(review);
                restaurant.PriceLevel = (int)Math.Round(reviews.Average(review => review.Price));
                restaurant.Rating = (int)Math.Round(reviews.Average(review => review.Stars));
                _context.Update(restaurant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Stars,Price,UserId,RestaurantId")] Review review)
        {
            if (id != review.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(review);
                    var restaurant = _context.Restaurant.First(r => r.Id == review.RestaurantId);
                    List<Review> reviews = _context.Review.Where(dBreview => dBreview.RestaurantId == review.RestaurantId).ToList();
                    reviews.Add(review);
                    restaurant.PriceLevel = (int)Math.Round(reviews.Average(review => review.Price));
                    restaurant.Rating = (int)Math.Round(reviews.Average(review => review.Stars));
                    _context.Update(restaurant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Review
                .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            var restaurant = _context.Restaurant.First(r => r.Id == review.RestaurantId);
            List<Review> reviews = _context.Review.Where(dBreview => dBreview.RestaurantId == review.RestaurantId).ToList();
            if (reviews.Count > 0)
            {
                restaurant.PriceLevel = (int)Math.Round(reviews.Average(review => review.Price));
                restaurant.Rating = (int)Math.Round(reviews.Average(review => review.Stars));
            }
            _context.Update(restaurant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
