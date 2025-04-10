using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Users
{
    public class LoginModel : PageModel
    {
        [BindProperty] public string UserName { get; set; }
        [BindProperty] public string PassWord { get; set; }
        public string Message { get; set; }
        private IUserServiceAsync _userService;
        public LoginModel(IUserServiceAsync userservice)
        {
            _userService = userservice;
        }
        public void OnGet()
        {
        }

        public void OnGetLogout()
        {
            HttpContext.Session.Remove("UserName");
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
                User loginUser = await _userService.VerifyUserAsync(UserName, PassWord);
                if (loginUser != null)
                {
                    HttpContext.Session.SetString("UserName", loginUser.userName);
                    return RedirectToPage("/index");
                }
                else
                {
                    Message = "Invalid username or password";
                    UserName = "";
                    PassWord = "";
                    return Page();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return RedirectToPage("/Users/Login");
        }
    }
}
