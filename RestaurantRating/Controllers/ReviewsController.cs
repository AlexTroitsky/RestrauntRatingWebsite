using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using RestaurantRating.Data;
using RestaurantRating.Migrations;
using RestaurantRating.Models;

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
        public async Task<IActionResult> IndexOfRestaurant(int id)
        {
            var reviews = await _context.Review
                .Where(r => r.RestaurantId == id)
                //.Include(o => o.Restaurant)
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
            //ViewBag.Restaurants = new SelectList(_context.Restaurant.ToList(), "Id", "Name");
            Review review = new Review();
            review.Restaurant = restaurant;
            return View(review);
        }

        
        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,Stars,UserId,RestaurantId")] Review review)
        {
            if (ModelState.IsValid)
            {
                review.Date = DateTime.Now;
                review.Restaurant = _context.Restaurant.First(r => r.Id == review.RestaurantId);
                review.User = _context.User.First(u => u.Id == review.UserId);
                _context.Add(review);
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,Stars,Date")] Review review)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Review.FindAsync(id);
            _context.Review.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReviewExists(int id)
        {
            return _context.Review.Any(e => e.Id == id);
        }
    }
}
