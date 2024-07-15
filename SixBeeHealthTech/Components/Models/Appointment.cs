using System.ComponentModel.DataAnnotations;

namespace SixBeeHealthTech.Components.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public TimeSpan AppointmentTime { get; set; }

        [Required]
        public string ContactNumber { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        public bool IsApproved { get; set; } = false;
    }
}
