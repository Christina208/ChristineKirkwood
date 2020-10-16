using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Requests
{
    public class ContactUsRequest
    {
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string From { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Please provide your Name.")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Please provide a subject.")]
        public string Subject { get; set; }
        [Required]
        [StringLength(1500, ErrorMessage = "Please provide a message.")]
        public string Message { get; set; }
    }
}
