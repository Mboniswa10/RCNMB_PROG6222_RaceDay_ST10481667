
using System;

namespace RaceDay.Mvc.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? EventType { get; set; }

        public DateOnly EventDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public string? Venue { get; set; }

        public string? City { get; set; }

        public string? Province { get; set; }

        public decimal DistanceKm { get; set; }

        public int MaxParticipants { get; set; }

        public decimal EntryFee { get; set; }

        public string? Status { get; set; }
    }
}
