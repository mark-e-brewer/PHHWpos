namespace PHHWpos.Models
{
    public class Review
    {
        public int? Id { get; set; }
        public int? Rating { get; set; }
        public int? UserId { get; set; }
        public int? ItemId { get; set; }
        public User User { get; set; }
        public Item Item { get; set; }
    }
}
