using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name cannot have less then 2 characters and more than 50 characters in length")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last Name cannot have less then 2 characters and more than 50 characters in length")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Zip code is required")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Zipcode cannot have less then 5 characters and more than 10 characters in length")]
        public string ZipCode { get; set; }

        [StringLength(15, ErrorMessage = "Phonenumber cant be more than 15 characters in length")]
        public string PhoneNumber { get; set; }
    }
}
