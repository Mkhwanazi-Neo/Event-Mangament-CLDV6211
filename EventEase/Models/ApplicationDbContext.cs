using EventEase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;

namespace EventEase.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Venue> Venue { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<Booking> Booking { get; set; }
        public DbSet<EventType> EventType { get; set; }
        public DbSet<BookingDetailsView> BookingDetailsView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure relationships
            modelBuilder.Entity<EventType>().HasData(
                new EventType { EventTypeID = 1, Name = "Conference" },
                new EventType { EventTypeID = 2, Name = "Wedding" },
                new EventType { EventTypeID = 3, Name = "Concert" },
                new EventType { EventTypeID = 4, Name = "expo" },
                new EventType { EventTypeID = 5, Name = "Seminar" }
            );

            base.OnModelCreating(modelBuilder);
            // Configure relationships
            modelBuilder.Entity<BookingDetailsView>()
                .ToView("vw_BookingDetails")
                .HasNoKey();

        }

       
        }
}

