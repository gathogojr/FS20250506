﻿namespace FS20250506.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
