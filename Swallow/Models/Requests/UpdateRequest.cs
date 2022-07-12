using System;
using System.ComponentModel.DataAnnotations;

namespace Swallow.Models.Requests;

    public class UpdateRequest
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }


        public string Username { get; set; }

        private string _password;

        [MinLength(6)]
        public string Password { get => _password; set => _password = replaceEmptyWithNull(value); }

        private string _confirmPassword;
        [Compare("Password")]
        public string ConfirmPassword{ get => _confirmPassword; set => _confirmPassword = replaceEmptyWithNull(value); }

        public string Occupation { get; set; }

        public string IsAdmin { get; set; }

        public string TokenExpiry { get; set; }

        // helpers
        private string replaceEmptyWithNull(string value)
        {
            // replace empty string with
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
