
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RaceDay.Api.Models
{
    [Table("Results")]
    public class Result
    {
        [Key]
        public int ResultId { get; set; }

        public int EnrolmentId { get; set; }

        public int? FinishPosition { get; set; }

        public int? FinishTimeSeconds { get; set; }

        public int? PaceSecondsPerKm { get; set; }

        [Required]
        [MaxLength(20)]
        public string ResultStatus { get; set; } = "Finished";

        public Enrolment? Enrolment { get; set; }
    }
}
