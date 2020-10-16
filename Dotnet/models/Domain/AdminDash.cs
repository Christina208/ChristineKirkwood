using System;
using System.Collections.Generic;
using System.Text;

namespace Sabio.Models.Domain
{
    public class AdminDash
    {
        public int ActiveUser { get; set; }
        public int Groups { get; set; }
        public int Organizations { get; set; }
        public int Plans { get; set; }
        public List<Plan> PlanFavorites { get; set; }
        public List<Group> FollowedGroups { get; set; }
        public List<UserProfile> RecentUsers { get; set; }

    }
}
