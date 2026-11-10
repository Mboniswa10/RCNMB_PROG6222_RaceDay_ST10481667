
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(80)]
        public string CategoryName { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
            = new List<Enrolment>();

        public ICollection<EventCategory> EventCategories { get; set; }
            = new List<EventCategory>();
    }
}
