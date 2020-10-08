using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRating.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required", AllowEmptyStrings = false)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        public bool isAdmin { get; set; }
    }
}
