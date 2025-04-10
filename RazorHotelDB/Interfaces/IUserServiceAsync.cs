using RazorHotelDB.Models;

namespace RazorHotelDB.Interfaces
{
    public interface IUserServiceAsync
    {
        Task<bool> AddUserAsync(User newUser);
        Task<List<User>> GetAllUsersAsync();
        Task<User> VerifyUserAsync(string userName, string passWord);
        Task<bool> VerifyAdminAsync(string userName);
    }
}
