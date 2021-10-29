using SQLite;
using System;
using System.Collections.Generic;

namespace fridge_management.Models
{

    public class ShoppingList
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Item> Items { get; set; }
    }
}
