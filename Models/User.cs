namespace PHHWpos.Models
{
    public class User
    {
        public int? Id { get; set; }
        public int? UID { get; set; }
        public string? Name { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
