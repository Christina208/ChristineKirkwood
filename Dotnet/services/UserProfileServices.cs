using Microsoft.AspNetCore.Mvc;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.Domain;
using Sabio.Models.Requests.UserProfiles;
using Sabio.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Sabio.Services
{
    public class UserProfileServices : IUserProfileService
    {
        IDataProvider _data = null;
        IDataProvider _dataprovider = null;
        public UserProfileServices(IDataProvider data, IDataProvider dataprovider)/*pss in instance of dataprovider*/
        {
            _data = data;

            _dataprovider = dataprovider;


        }
        public int Add(UserProfileAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[UserProfiles_Insert]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@UserId", userId);
                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    col.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@id"].Value;
                    int.TryParse(oId.ToString(), out id);
                    Console.WriteLine("Add Request works!");
                });
            return id;
        }    
        public void Update(UserProfilesUpdateRequest model)
        {
            string procName = "[dbo].[UserProfiles_Update]";
            _data.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    AddCommonParams(model, col);
                    col.AddWithValue("@Id", model.Id);
                },
                returnParameters: null);
        }
        public UserProfile Get(int id)
        {
            string procName = "[dbo].[UserProfiles_SelectById]";
            UserProfile userProfile = null;
            _data.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);

            }, delegate (IDataReader reader, short set)
            {
                int index;
                userProfile = MapProfile(reader, out index);
            }
            );
            return userProfile;
        }
        public void Delete(int id)
        {
            string procName = "[UserProfiles_DeleteById]";
            _data.ExecuteNonQuery(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Id", id);
            }
            );
        }
        public Paged<UserProfile> GetPagination(int pageIndex, int pageSize)
    {
        Paged<UserProfile> pagedResult = null;
        List<UserProfile> result = null;
        int totalCount = 0;
            _dataprovider.ExecuteCmd(
            "dbo.UserProfiles_SelectAllPaginated_V2",
            inputParamMapper: delegate (SqlParameterCollection parameterCollection)
            {
                parameterCollection.AddWithValue("@PageIndex", pageIndex);
                parameterCollection.AddWithValue("@PageSize", pageSize);
            },
            singleRecordMapper: delegate (IDataReader reader, short set)
            {
                int index;
                UserProfile model = MapProfile(reader, out index);
                model.UserStatusId = reader.GetSafeInt32(index++);
                if (totalCount == 0)
                {
                    totalCount = reader.GetSafeInt32(index++);
                }
                if (result == null)
                {
                    result = new List<UserProfile>();
                }
                result.Add(model);
            }
        );
        if (result != null)
        {
            pagedResult = new Paged<UserProfile>(result, pageIndex, pageSize, totalCount);
        }
        return pagedResult;
    }

        public Paged<UserProfile> GetQueryPaginate(int pageIndex, int pageSize, string query)
        {
            Paged<UserProfile> pagedResult = null;
            List<UserProfile> result = null;
            int totalCount = 0;

            _data.ExecuteCmd("[dbo].[UserProfiles_Select_All_Paginated_Search]",
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@pageIndex", pageIndex);
                    collection.AddWithValue("@pageSize", pageSize);
                    collection.AddWithValue("@query", query);

                },
                singleRecordMapper: delegate (IDataReader reader, short set)
                {
                    UserProfile userProfile = MapProfile(reader, out int startingIndex);
                    userProfile.UserStatusId = reader.GetSafeInt32(startingIndex++);
                    if (totalCount == 0)
                    {
                        totalCount = reader.GetSafeInt32(startingIndex);
                    }
                    if (result == null)
                    {
                        result = new List<UserProfile>();
                    }
                    result.Add(userProfile);
                });
            if (result != null)
            {
                pagedResult = new Paged<UserProfile>(result, pageIndex, pageSize, totalCount);
            }
            return pagedResult;
        }

        public static void AddCommonParams(UserProfileAddRequest model, SqlParameterCollection col)
        {
            col.AddWithValue("@FirstName", model.FirstName);
            col.AddWithValue("@LastName", model.LastName);
            col.AddWithValue("@Mi", model.MI);
            col.AddWithValue("@AvatarUrl", model.AvatarUrl);
        }

        private static UserProfile MapProfile(IDataReader reader, out int index)
        {
            UserProfile model = new UserProfile();
            index = 0;
            model.Id = reader.GetSafeInt32(index++);
            model.UserId = reader.GetSafeInt32(index++);
            model.FirstName = reader.GetSafeString(index++);
            model.LastName = reader.GetSafeString(index++);
            model.Mi = reader.GetSafeString(index++);
            model.AvatarUrl = reader.GetSafeString(index++);

            return model;
        }        
    }
}
