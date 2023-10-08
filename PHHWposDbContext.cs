﻿using PHHWpos.Models;
using Microsoft.EntityFrameworkCore;

namespace PHHWpos
{
    public class PHHWposDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public PHHWposDbContext(DbContextOptions<PHHWposDbContext> context) : base(context) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UID = 1001, Name = "John Doe" },
                new User { Id = 2, UID = 1002, Name = "Jane Smith" }
);

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Pizza", Price = 10 },
                new Item { Id = 2, Name = "Wings", Price = 8 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Name = "Order 1", UserId = 1, Status = true, Type = "Dine-In", CustomerPhone = 1234567890, CustomerEmail = "johndoe@example.com", PaymentType = "Cash", Tip = 2, DateClosed = DateTime.Now },
                new Order { Id = 2, Name = "Order 2", UserId = 2, Status = true, Type = "Takeout", CustomerPhone = 9197025135, CustomerEmail = "janesmith@example.com", PaymentType = "Credit Card", Tip = 3, DateClosed = DateTime.Now }
            );

            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, Rating = 5, UserId = 1, ItemId = 1 },
                new Review { Id = 2, Rating = 4, UserId = 2, ItemId = 2 }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}