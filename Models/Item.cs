﻿namespace PHHWpos.Models
{
    public class Item
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public int? Price { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
