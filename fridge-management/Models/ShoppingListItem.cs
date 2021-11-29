using SQLite;
using System;
using System.Collections.Generic;

namespace fridge_management.Models
{
    /// <summary>
    ///     Model which contains a ShoppingListItem
    /// </summary>
    public class ShoppingListItem : IModel
    {
        /// <summary>
        /// Contains an global unique identifier
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        /// <summary>
        /// Contains the name of the item
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Contains the name of the item
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Contains the amount of the item
        /// </summary>
        public int Amount { get; set; }
        /// <summary>
        /// Contains the number of the assigned shopping list
        /// </summary>
        public int ShoppingListNummer { get; set; }
    }
}
