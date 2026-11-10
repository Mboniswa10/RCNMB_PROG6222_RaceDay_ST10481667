
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("Enrolments")]
    public class Enrolment
    {
        [Key]
        public int EnrolmentId { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DateTime EnrolmentDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Enrolled";

        [ForeignKey(nameof(EventId))]
        public Event? Event { get; set; }

        [ForeignKey(nameof(ParticipantId))]
        public AppUser? Participant { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category? Category { get; set; }
    }
}
