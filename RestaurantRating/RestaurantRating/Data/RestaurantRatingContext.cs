using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRating.Models;

namespace RestaurantRating.Data
{
    public class RestaurantRatingContext : DbContext
    {
        public RestaurantRatingContext (DbContextOptions<RestaurantRatingContext> options)
            : base(options)
        {
        }

        public DbSet<RestaurantRating.Models.Restaurant> Restaurant { get; set; }

        public DbSet<RestaurantRating.Models.User> User { get; set; }

        public DbSet<RestaurantRating.Models.Review> Review { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                Username = "test",
                Password = "1234",
                UserType = UserType.Admin

            });
        }
    }
}
