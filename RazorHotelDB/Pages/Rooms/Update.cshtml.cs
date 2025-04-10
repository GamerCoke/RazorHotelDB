using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Pages.Rooms
{
    public class UpdateModel : PageModel
    {
        private IRoomServiceAsync _roomServiceAsync;
        public int OldHID { get; set; }
        public int OldRID { get; set; }
        public double OldPrice { get; set; }
        public string OldType { get; set; }
        [BindProperty]
        public int NewHID { get; set; }
        [BindProperty]
        public int NewRID { get; set; }
        [BindProperty]
        public double NewPrice { get; set; }
        [BindProperty]
        public string NewType { get; set; }
        public Room? UpdatedRoom { get; private set; }
        public UpdateModel(IRoomServiceAsync roomServiceAsync)
        {
            _roomServiceAsync = roomServiceAsync;
            OldHID = 0;
            OldRID = 0;
            OldPrice = 0;
            OldType = "";
            NewHID = 0;
            NewRID = 0;
            NewPrice = 0;
            NewType = "";
            UpdatedRoom = null;
        }

        public async Task OnGet(int hotelNo, int roomNo)
        {
            Room OldRoom = await _roomServiceAsync.GetRoomFromIdAsync(hotelNo, roomNo);
            OldHID = OldRoom.HotelNr;
            OldRID = OldRoom.VærelseNr;
            OldType = OldRoom.VærelseType;
            OldPrice = OldRoom.Pris;
            UpdatedRoom = null;
        }

        public async Task OnPost(int oldHotelNo, int oldRoomNo)
        {
            Room NewRoom = new Room(NewHID, NewRID, NewType, NewPrice);
            bool IsUpdated = await _roomServiceAsync.UpdateRoomAsync(NewRoom);
            if (!IsUpdated)
            {
                Room OldRoom = await _roomServiceAsync.GetRoomFromIdAsync(oldRoomNo, oldHotelNo);
                OldHID = OldRoom.HotelNr;
                OldRID = OldRoom.VærelseNr;
                OldType = OldRoom.VærelseType;
                OldPrice = OldRoom.Pris;
                UpdatedRoom = null;
            }
            else
                UpdatedRoom = NewRoom;
        }
    }
}
