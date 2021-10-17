using SQLite;
using System;

namespace fridge_management.Models
{
    public class FridgeItem
    {
        [PrimaryKey, AutoIncrement]
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationDate{ get; set; }
    }
}