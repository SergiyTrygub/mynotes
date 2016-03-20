using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class WishItem : IEntity<int>
    {
        public int Id { get; set; }

        public int WishDayId { get; set; }

        [JsonIgnore]
        public WishDay WishDay { get; set; }

        public string Text { get; set; }

        public int Position { get; set; }

        public bool IsDeleted { get; set; }

        public bool? IsCompleted { get; set; }

        public byte[] Timestamp { get; set; }

        public List<WishItemTag> Tags { get; set; }
    }
}
