using System;
using MyNotes.Web.Models;

namespace MyNotes.Web.MultiTenancy
{
    public class AppTenant : IEntity<string>
    {
        public string Id { get; set; }

        public string DisplayName { get; set; }

        public string IpAddress { get; set; }

        public bool IsPrivate { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
