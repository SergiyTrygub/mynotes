using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public interface IEntity<T>
    {
        T Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
