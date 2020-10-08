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
        [ForeignKey("User")]
        public int UserId { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
        [Range(1, 10)]
        public int Stars { get; set; }
        public DateTime Date { get; set; }

        public virtual User User { get; set; }
        public virtual Restaurant Restaurant { get; set; }
    }
}
