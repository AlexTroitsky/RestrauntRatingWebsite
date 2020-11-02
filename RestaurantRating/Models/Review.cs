using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRating.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        [MaxLength(100)]
        [Required]
        public string Content { get; set; }
        [Range(1, 5)]
        [Required]
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; }

    }
}
