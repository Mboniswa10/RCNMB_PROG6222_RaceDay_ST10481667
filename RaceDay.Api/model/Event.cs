
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("Events")]
    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public int OrganiserId { get; set; }

        [Required]
        [MaxLength(150)]
        public string EventName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string EventType { get; set; } = string.Empty;

        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; }

        [Required]
        [MaxLength(150)]
        public string Venue { get; set; } = string.Empty;

        [Required]
        [MaxLength(80)]
        public string City { get; set; } = string.Empty;

        [Required]
        [MaxLength(80)]
        public string Province { get; set; } = string.Empty;

        public decimal DistanceKm { get; set; }

        public int MaxParticipants { get; set; }

        public decimal EntryFee { get; set; }

        [MaxLength(500)]
        public string? RouteInformation { get; set; }

        [MaxLength(500)]
        public string? WeatherInformation { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Open";

        [ForeignKey(nameof(OrganiserId))]
        public AppUser? Organiser { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
            = new List<Enrolment>();
    }
}
