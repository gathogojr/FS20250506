namespace FS20250506.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Amount { get; set; }
        public Customer? Customer { get; set; }
    }
}
