using System;
using System.ComponentModel.DataAnnotations;

namespace Swallow.Models.Requests
{
    public class CreateRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string Occupation { get; set; }

        [Required]
        public string IsAdmin { get; set; }

        [Required]
        public DateTime TokenExpiry { get; set; }

        public override string ToString()
        {
            return Name + " " + Username;
        }

    }
}
