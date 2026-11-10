
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("Users")]
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = "Participant";

        public DateTime CreatedAt { get; set; }

        // Events organised by this user
        public ICollection<Event> OrganisedEvents { get; set; }
            = new List<Event>();

        // Enrolments made by this participant
        public ICollection<Enrolment> Enrolments { get; set; }
            = new List<Enrolment>();
    }
}
