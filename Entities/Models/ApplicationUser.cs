using Microsoft.AspNetCore.Identity;
using System;


namespace Entities.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
       
        public bool IsActive { get; set; }

        public  string Password { get; set; }

        public DateTime RegistrationDate { get; set; }

    }
}
