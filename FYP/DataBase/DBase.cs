using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dashboard;

namespace DataBase
{
    public class DBase:IdentityDbContext<ApplicationUser>
    {
        public DBase(DbContextOptions<DBase> options) : base(options)
        {
        }

        public DbSet<ContactUsModel> ContactUs { get; set; }
        public DbSet<Mail> Mail { get; set; }
        public DbSet<news_events> News_Events { get; set; }
       
    }
}