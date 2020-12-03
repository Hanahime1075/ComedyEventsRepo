using ComedyEvents.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComedyEvents.Context
{
    public class EventContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public EventContext(IConfiguration configuration,DbContextOptions options) : base (options)
        {
            _configuration = configuration;
        }

        public DbSet<Event> events { get; set; }
        public DbSet<Comedian> comedians { get; set; }
        public DbSet<Gig> gigs { get; set; }
        public DbSet<Venue> venues { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ComedyEvent")); 
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Event>()
                .HasData(new
                {
                    EventID = 1,
                    EventName = "Comedy Night",
                    EventDate = new DateTime(2019,05,19),
                    VenueID = 1
                });
            builder.Entity<Venue>()
                .HasData(new
                {
                    VenueID = 1,
                    VenueName = "Mohegun Sun",
                    Street = "123 Doa Street",
                    City = "Wikes pron",
                    State = "PA",
                    ZipCode = "12382",
                    Seating = 125,
                    ServesAlcohol = true
                });
            builder.Entity<Gig>()
                .HasData(new
                {
                    GigID = 1,
                    EventId = 1,
                    ComedianID = 1,
                    GigHeadline = "Pavols show",
                    GigLengthInMinutes = 60
                },new
                {
                    GigID = 2,
                    EventId = 1,
                    ComedianID = 2,
                    GigHeadline = "Robin show",
                    GigLengthInMinutes = 45
                });
            builder.Entity<Comedian>()
                .HasData(new
                {
                    ComedianID = 1,
                    FirstName = "Pavol",
                    LastName = "Almasi",
                    ContactPhone = "112345332"
                },new
                {
                    ComedianID = 2,
                    FirstName = "Robin",
                    LastName = "William",
                    ContactPhone = "442512748"
                });
        }
    }
}
