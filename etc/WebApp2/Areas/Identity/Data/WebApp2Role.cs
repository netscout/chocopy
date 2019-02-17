using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebApp2.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the WebApp2User class
    public class WebApp2Role : IdentityRole
    {
        public WebApp2Role(string roleName)
            :base(roleName)
        {

        }
    }
}
