using SQLite;
using System;

namespace fridge_management.Models
{
    public class FridgeItem : IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Amount { get; set; }
    }
}