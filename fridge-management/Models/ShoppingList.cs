﻿using SQLite;
using System.Collections.Generic;

namespace fridge_management.Models
{
    public class ShoppingList :  IModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
    }
}
