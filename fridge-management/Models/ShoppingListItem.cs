using SQLite;
using System;
using System.Collections.Generic;

namespace fridge_management.Models
{
    public class ShoppingListItem : IModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Amount { get; set; }
        public int ShoppingListNummer { get; set; }
    }
}
