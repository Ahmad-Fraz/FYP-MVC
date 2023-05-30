using FYP.Models.Dashboard;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Dashboard;

namespace DataBase
{
    public class DBase : IdentityDbContext<ApplicationUser>
    {
        public DBase(DbContextOptions<DBase> options) : base(options)
        {
        }

        public DbSet<ContactUsModel> ContactUs { get; set; }
        public DbSet<Mail> Mail { get; set; }
        public DbSet<news_events> News_Events { get; set; }
        public DbSet<Links> Links { get; set; }
        public DbSet<Assignments> Assignments { get; set; }
        public DbSet<Notes> MyNotes { get; set; }
        public DbSet<Quizz> Quizzs { get; set; }
        public DbSet<FYP.Models.Dashboard.Discussion> Discussion { get; set; } = default!;
        public DbSet<FYP.Models.Dashboard.Answer> Answers { get; set; } 
        public DbSet<FYP.Models.Dashboard.CourseList> Course { get; set; }
        public DbSet<Enroll> Enroll {  get; set; }
    }


}