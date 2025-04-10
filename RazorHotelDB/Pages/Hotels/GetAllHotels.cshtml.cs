using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        private IHotelServiceAsync _hotelService;

        public List<Hotel> Hotels { get; set; }
        public string? FilterString { get; set; }
        public GetAllHotelsModel(IHotelServiceAsync hotelService)
        {
            _hotelService = hotelService;
        }
        public async Task<ActionResult> OnGetAsync()
        {
            string UserName = HttpContext.Session.GetString("UserName");
            if (UserName == null)
            {
                return RedirectToPage("/Users/Login");
            }
            try
            {
                Hotels = await _hotelService.GetAllHotelAsync();
            }
            catch (Exception ex)
            {
                Hotels = new List<Models.Hotel>();
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
            
            FilterString = null;
        }
        public async Task OnPostFilter(string? name)
        {
            FilterString = name;
            if (name != null)
                Hotels = await _hotelService.GetHotelsByNameAsync(name);
            else
                Hotels = await _hotelService.GetAllHotelAsync();
        }
    }
}
