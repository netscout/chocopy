using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApp2.Areas.Identity.Data;
using WebApp2.Data;

namespace WebApp2.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly WebApp2.Areas.Identity.Data.WebApp2IdentityDbContext _context;

        public DetailsModel(WebApp2.Areas.Identity.Data.WebApp2IdentityDbContext context)
        {
            _context = context;
        }

        public Contact Contact { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Contact = await _context.Contact.FirstOrDefaultAsync(m => m.ContactId == id);

            if (Contact == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
