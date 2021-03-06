using SQLite;
using System;
using System.Collections.Generic;

namespace fridge_management.Models
{
    /// <summary>
    ///     Model which contains data of shopping lists
    /// </summary>
    public class ShoppingList :  IModel
    {
        /// <summary>
        ///     Contains an global unique identifier
        /// </summary>
        [PrimaryKey]
        public Guid Id { get; set; }
        /// <summary>
        /// Contains the name of the shopping list
        /// </summary>
        public string Text { get; set; }

    }
}
