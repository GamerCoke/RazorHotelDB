using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private IRoomServiceAsync _roomServiceAsync;
        public Room? Room { get; private set; }
        public bool IsDeleted { get; private set; }
        public DeleteModel(IRoomServiceAsync hotelServiceAsync)
        {
            _roomServiceAsync = hotelServiceAsync;
            Room = null;
            IsDeleted = false;
        }

        public async Task OnGet(int hotelNo, int roomNo)
        {
            Room = await _roomServiceAsync.GetRoomFromIdAsync(roomNo, hotelNo);
            IsDeleted = false;
        }
        public async Task OnPost(int hotelNo, int roomNo)
        {
            Room = await _roomServiceAsync.DeleteRoomAsync(roomNo, hotelNo);
            IsDeleted = Room != null;
        }
    }
}
