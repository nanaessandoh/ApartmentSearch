using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentSearch.Data
{
    public class ApplicationUser
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
