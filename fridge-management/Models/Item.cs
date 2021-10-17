using System;

namespace fridge_management.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}