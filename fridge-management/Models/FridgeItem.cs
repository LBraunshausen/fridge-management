﻿using SQLite;
using System;

namespace fridge_management.Models
{
    /// <summary>
    ///     Model which contains a FridgeItem
    /// </summary>
    public class FridgeItem : IModel
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Amount { get; set; }
    }
}