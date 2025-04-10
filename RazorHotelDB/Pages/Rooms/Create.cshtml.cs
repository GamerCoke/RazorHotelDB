using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private IRoomServiceAsync _roomService { get; set; }
        public bool IsCreated { get; set; }
        [BindProperty]
        public Room NewRoom { get; set; }
        public CreateModel(IRoomServiceAsync roomServiceAsync)
        {
            _roomService = roomServiceAsync;
            NewRoom = new();
            IsCreated = false;
        }
        public void OnGet()
        {
            NewRoom = new();
            IsCreated = false;
        }
        public async Task OnPost()
        {

            IsCreated = await _roomService.CreateRoomAsync(NewRoom);
        }
    }
}
