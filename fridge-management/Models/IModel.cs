using System;
using System.Collections.Generic;
using System.Text;

namespace fridge_management.Models
{
    public interface IModel
    {
        /// <summary>
        ///     Contains an global unique identifier
        /// </summary>
        Guid Id { get; set; }
    }
}
