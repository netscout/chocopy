using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp2.Areas.Identity.Data;
using WebApp2.Data;

namespace WebApp2.Pages.Contacts
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly WebApp2.Areas.Identity.Data.WebApp2IdentityDbContext _context;

        public IndexModel(WebApp2.Areas.Identity.Data.WebApp2IdentityDbContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get;set; }

        public async Task OnGetAsync()
        {
            Contact = await _context.Contact.ToListAsync();
        }
    }
}
