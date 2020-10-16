using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models.Domain;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sabio.Services
{
    public class AdminDashServices: IAdminDashServices
    {
        IDataProvider _data = null;
        public AdminDashServices(IDataProvider data)
        {
            _data = data;
        }
        public List<AdminDash> Get()
        {
            
            string procName = "[dbo].[Users_SelectAdminDashboardData]";
            List<AdminDash> adminDash = null;

            _data.ExecuteCmd(procName, inputParamMapper: null,
                singleRecordMapper: delegate (IDataReader reader, short set)
           {
               AdminDash dashCount = new AdminDash();
               int startingIndex = 0;

               dashCount.ActiveUser = reader.GetInt32(startingIndex++);
               dashCount.Groups = reader.GetInt32(startingIndex++);
               dashCount.Organizations = reader.GetInt32(startingIndex++);
               dashCount.Plans = reader.GetInt32(startingIndex++);
               dashCount.PlanFavorites = reader.DeserializeObject<List<Plan>>(startingIndex++);
               dashCount.FollowedGroups = reader.DeserializeObject<List<Group>>(startingIndex++);
               dashCount.RecentUsers = reader.DeserializeObject<List<UserProfile>>(startingIndex++);

               if (adminDash == null)
               {
                   adminDash = new List<AdminDash>();
               }
               adminDash.Add(dashCount);
           },
                returnParameters: null
                ) ;
            return adminDash;


        }
    }
}
