using System;
using System.ComponentModel.DataAnnotations;

namespace RaceDay.Mvc.Models
{
    public class CreateEventViewModel
    {
        [Required]
        [StringLength(150)]
        public string EventName { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string EventType { get; set; } = string.Empty;

        [Required]
        public DateOnly EventDate { get; set; }

        [Required]
        public TimeOnly StartTime { get; set; }

        [Required]
        public string Venue { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public string Province { get; set; } = string.Empty;

        [Range(0.01, 1000)]
        public decimal DistanceKm { get; set; }

        [Range(1, 100000)]
        public int MaxParticipants { get; set; }

        [Range(0, 100000)]
        public decimal EntryFee { get; set; }

        public string? RouteInformation { get; set; }

        public string? WeatherInformation { get; set; }
    }
}