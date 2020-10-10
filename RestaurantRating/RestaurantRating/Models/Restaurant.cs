using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace RestaurantRating.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Range(1, 5)]
        public int PriceLevel { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Address { get; set; }

        public byte[] Image { get; set; }

        
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
