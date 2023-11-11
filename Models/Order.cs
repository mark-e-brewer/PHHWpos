namespace PHHWpos.Models
{
    public class Order
    {
        public int? Id { get; set; }
        required
        public string? Name { get; set; }
        public int? UserId { get; set; }
        public bool? Status { get; set; }
        public string? Type { get; set; }
        public string? CustomerName { get; set; }
        public long? CustomerPhone { get; set; }
        public string? CustomerEmail { get; set; }
        public string? PaymentType { get; set; }
        public int? Tip { get; set; }
        public DateTime? DateClosed { get; set; }
        public User? User { get; set; }
        public ICollection<Item>? Items { get; set; }
    }
}
