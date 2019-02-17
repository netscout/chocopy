using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApp2.Areas.Identity.Data;
using WebApp2.Data;

namespace WebApp2.Areas.Identity.Data
{
    public class WebApp2IdentityDbContext : IdentityDbContext<WebApp2User>
    {
        public WebApp2IdentityDbContext(DbContextOptions<WebApp2IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
