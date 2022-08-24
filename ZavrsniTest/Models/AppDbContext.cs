using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZavrsniTest.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Agencija> Agencije { get; set; }
        public DbSet<Oglas> Oglasi { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Agencija>().HasData(
                    new Agencija { Id = 1, Naziv = "Naj Nekretnine", GodinaOsnivanja = 2005 },
                    new Agencija { Id = 2, Naziv = "Dupleks Nekretnine", GodinaOsnivanja = 2010 },
                    new Agencija { Id = 3, Naziv = "Fast Nekretnine", GodinaOsnivanja = 2005 }
                );
            builder.Entity<Oglas>().HasData(
                    new Oglas { Id = 1, Naslov = "Komforna porodicna kuca", Tip = "Kuca", GodinaIzgradnje = 1987, CenaNekretnine = 110000, AgencijaId = 3 },
                    new Oglas { Id = 2, Naslov = "Stan na ekstra lokaciji", Tip = "Stan", GodinaIzgradnje = 1979, CenaNekretnine = 80000, AgencijaId = 1 },
                    new Oglas { Id = 3, Naslov = "Moderan dupleks", Tip = "Dupleks stan", GodinaIzgradnje = 2020, CenaNekretnine = 220000, AgencijaId = 2 },
                    new Oglas { Id = 4, Naslov = "Povoljna vikendica", Tip = "Vikendica", GodinaIzgradnje = 1971, CenaNekretnine = 50000, AgencijaId = 1 },
                    new Oglas { Id = 5, Naslov = "Stan u sirem centru", Tip = "Stan", GodinaIzgradnje = 1955, CenaNekretnine = 90000, AgencijaId = 3 }
                );
            base.OnModelCreating(builder);
        }
    }
}
