﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyNotes.Web.Models
{
    public class WishDay : IEntity<int>
    {
        public int Id { get; set; }

        [Required]
        public string TenantId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string CreatorId { get; set; }

        public bool IsDeleted { get; set; }

        public List<WishItem> WishList { get; set; }

        public byte[] Timestamp { get; set; }
    }
}
