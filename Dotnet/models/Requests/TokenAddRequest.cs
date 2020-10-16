using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Requests
{
    public class TokenAddRequest
    {
        [Required]
        public Guid Token { get; set; }
        [Required]
        [EmailAddress()]
        public string Email { get; set; }
        [Required]
        public int TokenType { get; set; }
    }
}
