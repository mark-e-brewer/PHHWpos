using PHHWpos.Models;
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
                new User { Id = 1, UID = "b1s92873y7", Name = "Mark" },
                new User { Id = 2, UID = "n0c2389o4n", Name = "Alex" }
);

            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Pizza", Price = 10 },
                new Item { Id = 2, Name = "Wings", Price = 8 }
            );

            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, Name = "Order 1", UserId = 1, Status = false, Type = "Phone", CustomerName = "john doe", CustomerPhone = 1234567890, CustomerEmail = "johndoe@example.com", PaymentType = "Cash", Tip = 2, DateClosed = null },
                new Order { Id = 2, Name = "Order 2", UserId = 2, Status = false, Type = "In-Person", CustomerName = "Jane Smith", CustomerPhone = 9197025135, CustomerEmail = "janesmith@example.com", PaymentType = "Credit Card", Tip = 3, DateClosed = null }
            );


            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, Rating = 5, UserId = 1, ItemId = 1 },
                new Review { Id = 2, Rating = 4, UserId = 2, ItemId = 2 }
            );


            base.OnModelCreating(modelBuilder);
        }
    }
}
