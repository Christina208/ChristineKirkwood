using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Models.Requests.UserProfiles
{
    public class UserProfilesUpdateRequest : UserProfileAddRequest, IModelIdentifier
    {
        public int Id { get; set; }
    }
}
