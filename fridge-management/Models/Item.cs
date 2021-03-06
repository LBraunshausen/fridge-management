using SQLite;
using System;

namespace fridge_management.Models
{
    /// <summary>
    /// Model which contains an Item
    /// </summary>
    public class Item : IModel
    {
        /// <summary>
        /// Contains an global unique identifier
        /// </summary>
        [PrimaryKey]
        public Guid Id { get; set; }
        /// <summary>
        /// Contains the name of a FridgeItem
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Contains the expiration date of a FridgeItem
        /// </summary>
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Contains the amount date of a FridgeItem
        /// </summary>
        public int Amount { get; set; }
    }
}