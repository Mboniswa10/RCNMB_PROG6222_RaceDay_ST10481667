
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("EventCategories")]
    public class EventCategory
    {
        public int EventId { get; set; }

        public int CategoryId { get; set; }

        public Event? Event { get; set; }

        public Category? Category { get; set; }
    }
}
