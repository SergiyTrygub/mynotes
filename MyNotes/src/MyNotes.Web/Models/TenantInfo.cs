using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class TenantInfo
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDeleted { get; set; }
    }
}
