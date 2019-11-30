using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SamayaPatra.Common
{
    public class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Username Should be at least 8 characters long.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Password Should be at least 8 characters long.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm Password is required")]
        [StringLength(60, MinimumLength = 8, ErrorMessage = "Confirm Password Should be at least 8 characters long.")]
        [Compare("Password", ErrorMessage = "Confirm Password donot match with Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "User Role is required")]
        public int UserRoleID { get; set; }
        public string UserRole { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Contact should be exactly of 10 digits.")]
        public string Contact { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email address")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public int VehicleID { get; set; }
        public string VehicleNo { get; set; }
    }
}
