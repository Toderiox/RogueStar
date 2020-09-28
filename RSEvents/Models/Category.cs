using System;
using System.Collections.Generic;

#nullable disable

namespace RSEvents.Models
{
    public partial class Category
    {
        public Category()
        {
            Events = new HashSet<Event>();
        }

        public Guid CategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}
