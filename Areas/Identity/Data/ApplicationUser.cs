using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_book.Models;
using Microsoft.AspNetCore.Identity;

namespace asp_book.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; }
    public string DOB { get; set; }
    public string Phone { get; set; }
    public Faculty Faculty { get; set; }
    //public Group Groups { get; set; }
}

public class ApplicationRole : IdentityRole
{

}
