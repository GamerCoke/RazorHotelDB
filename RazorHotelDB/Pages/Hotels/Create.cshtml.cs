using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        private IHotelServiceAsync _hotelService { get; set; }
        public bool IsCreated { get; set; }
        [BindProperty]
        public Hotel NewHotel { get; set; }
        public CreateModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelService = hotelServiceAsync;
            NewHotel = new();
            IsCreated = false;
        }
        public void OnGet()
        {
            NewHotel = new();
            IsCreated = false;
        }
        public async Task OnPost()
        {
            
            IsCreated = await _hotelService.CreateHotelAsync(NewHotel);
        }
    }
}
