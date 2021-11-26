using System;
using System.Collections.Generic;
using System.Text;

namespace fridge_management.Models
{
    public interface IModel
    {
        Guid Id { get; set; }
    }
}
