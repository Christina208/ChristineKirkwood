using Microsoft.Extensions.Options;
using Sabio.Data;
using Sabio.Data.Providers;
using Sabio.Models;
using Sabio.Models.AppSettings;
using Sabio.Models.Domain;
using Sabio.Models.Requests;
using Sabio.Models.Requests.UserProfiles;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public class UserService : IUserService
    {
        private IAuthenticationService<int> _authenticationService;
        private IDataProvider _dataProvider;
        private AppKeys _appKeys;

        public UserService(IAuthenticationService<int> authSerice, IDataProvider dataProvider, IOptions<AppKeys> appKeys)
        {
            _authenticationService = authSerice;
            _dataProvider = dataProvider;
            _appKeys = appKeys.Value;
        }

        public async Task<bool> LogInAsync(string email, string password)
        {
            bool isSuccessful = false;

            IUserAuthData response = Get(email, password);

            if (response != null)
            {
                await _authenticationService.LogInAsync(response);
                isSuccessful = true;
            }

            return isSuccessful;
        }

        public async Task<bool> LogInTest(string email, string password, int id, string[] roles = null)
        {
            bool isSuccessful = false;
            var testRoles = new[] { "User", "Super", "Content Manager" };

            var allRoles = roles == null ? testRoles : testRoles.Concat(roles);

            IUserAuthData response = new UserBase
            {
                Id = id
                ,
                Name = email
                ,
                Roles = allRoles
                ,
                TenantId = "Acme Corp UId"
            };

            Claim fullName = new Claim("CustomClaim", "Sabio Bootcamp");
            await _authenticationService.LogInAsync(response, new Claim[] { fullName });

            return isSuccessful;
        }

        public int Create(object userModel)
        {
            //make sure the password column can hold long enough string. put it to 100 to be safe

            int userId = 0;
            string password = "Get from user model when you have a concreate class";
            string salt = BCrypt.BCryptHelper.GenerateSalt();
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(password, "");

            //DB provider call to create user and get us a user id

            //be sure to store both salt and passwordHash
            //DO NOT STORE the original password value that the user passed us

            return userId;
        }

        /// <summary>
        /// Gets the Data call to get a give user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        private IUserAuthData Get(string email, string password)
        {
            //make sure the password column can hold long enough string. put it to 100 to be safe
            string passwordFromDb = "";
            UserBase user = null;

            //get user object from db;

            bool isValidCredentials = BCrypt.BCryptHelper.CheckPassword(password, passwordFromDb);

            return user;
        }

        public void UserStatusUpdate(int id, int statusId)
		{
            string procName = "[dbo].[Users_UpdateStatus]";
            _dataProvider.ExecuteNonQuery(procName,
            inputParamMapper: delegate (SqlParameterCollection parameters)
            {
                parameters.AddWithValue("@Id", id);
                parameters.AddWithValue("@UserStatusId", statusId);


            }, returnParameters: null);
        }
        //verfiy 
        public bool VerifyEmail(string email)
        {
            string procName = "[dbo].[Users_VerifyEmail]";
            bool validEmail = false;
            _dataProvider.ExecuteCmd(procName, delegate (SqlParameterCollection paramCollection)
            {
                paramCollection.AddWithValue("@Email", email);
            }, delegate (IDataReader reader, short set)
            {
                string dbEmail = reader.GetSafeString(0);
                if (dbEmail != null)
                {
                    validEmail = true;
                }
            }
            );
            
            return validEmail;


        }
        public void InsertToken(TokenAddRequest model)
        {
       
            string procName = "[dbo].[UserTokens_InsertByEmail]";
            _dataProvider.ExecuteNonQuery(procName, delegate (SqlParameterCollection col)
            {
                col.AddWithValue("@UserToken", model.Token);
                col.AddWithValue("@Email", model.Email);
                col.AddWithValue("@TokenType", model.TokenType);
            }
            );
        }
        public void ResetPass(ResetPassAddRequest model)
        {

            string salt = BCrypt.BCryptHelper.GenerateSalt();
            string hashedPassword = BCrypt.BCryptHelper.HashPassword(model.Password, salt);

            string procName = "[dbo].[Users_ResetPasswordByToken]";
            _dataProvider.ExecuteNonQuery(procName,
                inputParamMapper: delegate (SqlParameterCollection col)
                {
                    col.AddWithValue("@Password", hashedPassword);
                    col.AddWithValue("@UserToken", model.Token);//token
                }
                );
        }
    }
}