using System;
using System.Collections.Generic;

#nullable disable

namespace RSEvents.Models
{
    public partial class EventUser
    {
        public Guid Id { get; set; }
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public bool? Attendance { get; set; }

        public virtual Event Event { get; set; }
    }
}
