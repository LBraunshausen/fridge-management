using SQLite;
using System;
using System.Collections.Generic;

namespace fridge_management.Models
{
    public class ShoppingList :  IModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public string Text { get; set; }
    }
}
