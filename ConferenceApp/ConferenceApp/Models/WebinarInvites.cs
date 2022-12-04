using System.ComponentModel.DataAnnotations;

namespace ConferenceApp.Models
{
    public class WebinarInvites
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your email address")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please specify wheter you will join")]
        public bool? WillJoin { get; set; }
    }
}
