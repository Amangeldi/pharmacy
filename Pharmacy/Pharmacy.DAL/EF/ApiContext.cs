using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pharmacy.DAL.EF
{
    public class ApiContext :IdentityDbContext<User>
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryLangLink> CategoryLangLinks { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryLangLink> CountryLangLinks { get; set; }
        public DbSet<Lang> Langs { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<MedicamentLangLink> MedicamentLangLinks { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsLangLink> NewsLangLinks { get; set; }
        public ApiContext(DbContextOptions<ApiContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
