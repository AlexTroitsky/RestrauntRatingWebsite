using Microsoft.AspNetCore.Components.Routing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRating.Models
{
    public enum UserType {
        Admin,
        Reveiwer
    }
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Username is required", AllowEmptyStrings = false)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required", AllowEmptyStrings = false)]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Type")]
        [Required(ErrorMessage = "Type is required")]
        public UserType? UserType { get; set; }
    }
}
