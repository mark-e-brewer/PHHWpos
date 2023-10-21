namespace PHHWpos.DTOs
{
    public class ClosedOrderSummaryDTO
    {
        public int? OrderId { get; set; }
        public int? Tip { get; set; }
        public string? OrderType { get; set; }
        public string? PaymentType { get; set; }
        public DateTime? DateClosed { get; set; }
        public List<ItemSummaryDTO> Items { get; set; }
    }

    public class ItemSummaryDTO
    {
        public string? Name { get; set; }
        public int? Price { get; set; }
    }
}
