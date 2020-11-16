using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantRating.Models;

namespace RestaurantRating.Data
{
    public class DBInitializer
    {
        public static void Initialize(RestaurantRatingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Restaurant.Any())
            {
                return;   // DB has been seeded
            }

            

            var restraunts= new Restaurant[]
            {

            new Restaurant{Id=1, Name="BBB", Address="HaMada, Giv'at Nof, Nes Ziona, Rehovot Subdistrict, Center District, no, Israel", City="Ness Ziona", Description="Burgers Burger Bar - New Meat Experience", Lat=31.9132456, Lon=34.8059771, Rating=5, PriceLevel=4, Image=System.IO.File.ReadAllBytes("wwwroot/images/BBB.png"), Reviews=new List<Review>{ } },
            new Restaurant{Id=2, Name="Pizza Hut", Address="Ha-Tsiyonut St 13, Ashdod", City="Ashdod", Description="Pizza Hut is an American restaurant chain and international franchise which was founded in 1958.", Lat=31.7977314, Lon=34.6529922, Rating=2, PriceLevel=3, Image=System.IO.File.ReadAllBytes("wwwroot/images/hut.png"), Reviews=new List<Review>{ } },
            new Restaurant{Id=3, Name="SOHO", Address="Moshe Beker St 15, Rishon LeTsiyon", City="Rishon LeTsiyon", Description="Sushi Bar", Lat=31.9737629, Lon=34.8083446, Rating=4, PriceLevel=5, Image=System.IO.File.ReadAllBytes("wwwroot/images/Soho.png"), Reviews=new List<Review>{ } },

            new Restaurant{Id=4, Name="Japanika", Address="HaArbaa, 2, HaArbaa, Tel Aviv, Sarona Gardens, Tel Aviv-Yafo, Tel Aviv District, no, Israel", City="Tel Aviv", Description="Asian Cuisine", Lat=32.0703169, Lon=34.7829342, Rating=5, PriceLevel=3, Image=System.IO.File.ReadAllBytes("wwwroot/images/Japanika.png"), Reviews=new List<Review>{ } },
            new Restaurant{Id=5, Name="Domino's Pizza", Address="Shalom Aleichem, Ziv, Haifa, Haifa Subdistrict, Haifa District, no, Israel", City="Haifa", Description="Pizza branded as Domino's, is an American multinational pizza restaurant chain founded in 1961.", Lat=32.7831577, Lon=35.0145085, Rating=2, PriceLevel=2, Image=System.IO.File.ReadAllBytes("wwwroot/images/Dominos.png"), Reviews=new List<Review>{ } },


            };
            foreach (Restaurant s in restraunts)
            {
                context.Restaurant.Add(s);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{Id = 1, Username = "test", Password = "1234", Address = "Rehovot, Rehovot Subdistrict, Center District, Israel", Lat = 31.8952532, Lon = 34.8105616, UserType = UserType.Admin},
                new User{Id = 2, Username = "Admin", Password = "1234", Address = "Rishon LeZion, Rehovot Subdistrict, Center District, no, Israel", Lat = 31.9635712, Lon = 34.8101149, UserType = UserType.Admin},
                new User{Id = 3, Username = "Alex", Password = "1234", Address = "Nes Ziona, Rehovot Subdistrict, Center District, Israel", Lat = 31.9295577, Lon = 34.7990609, UserType = UserType.Reviewer},
                new User{Id = 4, Username = "Tamar", Password = "1234", Address = "Ashdod, Ashkelon Subdistrict, South District, Israel", Lat = 31.7977314, Lon = 34.6529922, UserType = UserType.Reviewer},
                new User{Id = 5, Username = "Larry", Password = "1234", Address = "Bat Yam, Tel Aviv District, Israel", Lat = 32.0154565, Lon = 34.7505283, UserType = UserType.Reviewer},
                new User{Id = 6, Username = "User", Password = "1234", Address = "Tel Aviv Street 5, Haifa, Israel", Lat = 32.8246481, Lon = 34.9892144, UserType = UserType.Reviewer},

            };
            foreach (User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();

            var reviews = new Review[]
            {
            new Review{Id=1, Stars=5, Price=4, User=context.User.Find(1), Restaurant=context.Restaurant.Find(1), Content="Good Restraunt!!!", Date=DateTime.Parse("2020-09-01")},
            new Review{Id=2, Stars=5, Price=4, User=context.User.Find(2), Restaurant=context.Restaurant.Find(1), Content="Good Food!!!", Date=DateTime.Parse("2020-07-01")},
            new Review{Id=3, Stars=5, Price=4, User=context.User.Find(3), Restaurant=context.Restaurant.Find(1), Content="Nice Service!!!", Date=DateTime.Parse("2020-06-01")},



            new Review{Id=4, Stars=2, Price=3, User=context.User.Find(2), Restaurant=context.Restaurant.Find(2), Content="Bad Restraunt!!!", Date=DateTime.Parse("2020-08-01")},
            new Review{Id=5, Stars=2, Price=3, User=context.User.Find(3), Restaurant=context.Restaurant.Find(2), Content="Worst Experience!!!", Date=DateTime.Parse("2020-05-01")},
            new Review{Id=6, Stars=2, Price=3, User=context.User.Find(4), Restaurant=context.Restaurant.Find(2), Content="Worst Food!!!", Date=DateTime.Parse("2020-04-01")},
            new Review{Id=7, Stars=2, Price=3, User=context.User.Find(5), Restaurant=context.Restaurant.Find(2), Content="Not Happy :(", Date=DateTime.Parse("2020-08-01")},
            new Review{Id=8, Stars=2, Price=3, User=context.User.Find(6), Restaurant=context.Restaurant.Find(2), Content="Shit Experience!!!", Date=DateTime.Parse("2020-05-01")},



            new Review{Id=9, Stars=4, Price=5, User=context.User.Find(1), Restaurant=context.Restaurant.Find(3), Content="Good Restraunt!!!", Date=DateTime.Parse("2020-09-01")},
            new Review{Id=10, Stars=4, Price=5, User=context.User.Find(5), Restaurant=context.Restaurant.Find(3), Content="Best Sushi!!!", Date=DateTime.Parse("2020-07-01")},


            new Review{Id=11, Stars=5, Price=3, User=context.User.Find(1), Restaurant=context.Restaurant.Find(4), Content="Good Asian Restraunt!!!", Date=DateTime.Parse("2020-09-01")},
            new Review{Id=12, Stars=5, Price=3, User=context.User.Find(6), Restaurant=context.Restaurant.Find(4), Content="Good Food!!!", Date=DateTime.Parse("2020-07-01")},
            new Review{Id=13, Stars=5, Price=3, User=context.User.Find(3), Restaurant=context.Restaurant.Find(4), Content="Nice Service!!!", Date=DateTime.Parse("2020-06-01")},


            new Review{Id=14, Stars=2, Price=2, User=context.User.Find(5), Restaurant=context.Restaurant.Find(5), Content="Worst Pizza i've ever tasted!!!", Date=DateTime.Parse("2020-08-01")},
            };
            foreach (Review r in reviews)
            {
                context.Review.Add(r);
            }
            context.SaveChanges();
        }
    }
}
