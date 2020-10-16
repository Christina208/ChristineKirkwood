using Sabio.Models.Requests;
using System.Threading.Tasks;

namespace Sabio.Services
{
    public interface IUserService
    {
        int Create(object userModel);

        Task<bool> LogInAsync(string email, string password);
        public void UserStatusUpdate(int id, int statusId);
        Task<bool> LogInTest(string email, string password, int id, string[] roles = null);
        public bool VerifyEmail(string email);
        public void InsertToken(TokenAddRequest model);
        public void ResetPass(ResetPassAddRequest model);
    }
}