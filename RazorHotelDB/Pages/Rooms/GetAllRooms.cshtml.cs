using RazorHotelDB.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        private IRoomServiceAsync _roomService;

        public List<Room> Rooms { get; set; }
        public string? FilterString { get; set; }
        public GetAllRoomsModel(IRoomServiceAsync roomService)
        {
            _roomService = roomService;
        }
        public async Task OnGet()
        {
            Rooms = await _roomService.GetAllRoomAsync();
            FilterString = null;
        }
        public async Task OnPostFilter(string? name)
        {
            FilterString = name;
            if (name != null)
                Rooms = await _roomService.GetRoomsByHotelAsync(name);
            else
                Rooms = await _roomService.GetAllRoomAsync();
        }
    }
}
