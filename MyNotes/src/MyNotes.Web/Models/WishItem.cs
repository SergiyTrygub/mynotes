using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class WishItem
    {
        public int Id { get; set; }

        public int WishDayId { get; set; }

        public string Text { get; set; }

        public int Position { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsCompleted { get; set; }
    }
}
