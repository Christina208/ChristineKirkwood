using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.UserProfiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Services.Interfaces
{
    public interface IUserProfileService
    {
        int Add(UserProfileAddRequest model, int currentUserId);
        void Update(UserProfilesUpdateRequest model);
        UserProfile Get(int id);
        void Delete(int id);
        Paged<UserProfile> GetPagination(int pageIndex, int pageSize);
        Paged<UserProfile> GetQueryPaginate(int pageIndex, int pageSize, string query);
    }
}
