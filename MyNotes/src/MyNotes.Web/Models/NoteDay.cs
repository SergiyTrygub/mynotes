using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class NoteDay
    {
        public int Id { get; set; }

        public string TenantId { get; set; }

        public DateTime Date { get; set; }

        public string CreatorId { get; set; }

        public bool IsDeleted { get; set; }

        public IEnumerable<Note> Notes { get; set; }
    }
}
