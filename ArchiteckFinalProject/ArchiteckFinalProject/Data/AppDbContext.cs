using ArchiteckFinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchiteckFinalProject.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
           
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<HomeBanner> HomeBanners { get; set; }
        public DbSet<Indicator> Indicators { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<PersonPosition> PersonPositions { get; set; }
        public DbSet<PersonSocial> PersonSocials { get; set; }
        public DbSet<PersonToSocial> PersonToSocials { get; set; }
        public DbSet<Process> Processes { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCatagory> ServiceCatagories { get; set; }
        public DbSet<ServiceComment> ServiceComments { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<Solution> Solutions { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<Team> Teams { get; set; }

    }
}
