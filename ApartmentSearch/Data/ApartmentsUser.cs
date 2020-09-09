using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApartmentSearch.Data
{
    // Add profile data for application users by adding properties to the ApartmentSearchUser class
    public class ApartmentsUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        [MaxLength(15)]
        public override string PhoneNumber { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
