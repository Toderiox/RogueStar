using System;
using System.Collections.Generic;

#nullable disable

namespace RSEvents.Models
{
    public partial class Event
    {
        public Event()
        {
            EventUsers = new HashSet<EventUser>();
        }

        public Guid EventId { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<EventUser> EventUsers { get; set; }
    }
}
