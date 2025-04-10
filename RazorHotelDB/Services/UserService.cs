using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class UserService : IUserServiceAsync
    {
        public Task<bool> AddUserAsync(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task<List<User>> GetAllUsersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> VerifyAdminAsync(string userName)
        {
            return null;
        }

        public Task<User> VerifyUserAsync(string userName, string passWord)
        {
            List<User> users = await GetAllUsersAsync();
            foreach (User user in users)
            {
                if (userName.Equals(user.UserName) && passWord.Equals(user.Password))
                {
                    return user;
                }
            }
            return null;
        }
    }
}
