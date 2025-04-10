using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Models;
using RazorHotelDB.Interfaces;

namespace RazorHotelDB.Pages.Hotels
{
    public class DeleteModel : PageModel
    {
        private IHotelServiceAsync _hotelServiceAsync;
        public Hotel? Hotel { get; private set; }
        public bool IsDeleted { get; private set; }
        public DeleteModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelServiceAsync = hotelServiceAsync;
            Hotel = null;
            IsDeleted = false;
        }

        public async Task OnGet(int hotelNo)
        {
            Hotel = await _hotelServiceAsync.GetHotelFromIdAsync(hotelNo);
            IsDeleted = false;
        }
        public async Task OnPost(int hotelNo)
        {
            Hotel = await _hotelServiceAsync.DeleteHotelAsync(hotelNo);
            IsDeleted = Hotel != null;
        }
    }
}
