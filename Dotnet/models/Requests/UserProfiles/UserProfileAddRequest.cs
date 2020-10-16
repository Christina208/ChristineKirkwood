using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Sabio.Models.Requests.UserProfiles
{
    public class UserProfileAddRequest
    {
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }
        [StringLength(2, MinimumLength = 1)]
        public string MI { get; set; }
        [StringLength(255, MinimumLength = 2)]
        public string AvatarUrl { get; set; }
    }
}
