using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class WishItemTag : IEntity<int>
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] Timestamp { get; set; }

        public int WishItemId { get; set; }

        public WishItem WishItem { get; set; }
    }
}
