using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CollegeManagementSystem.Models
{
    public class User : IdentityUser
    {
        public string? city { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

    }
}
