using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;
using System.Security.Cryptography;

namespace RazorHotelDB.Pages.Hotels
{
    public class UpdateModel : PageModel
    {
        private IHotelServiceAsync _hotelServiceAsync;
        public int OldID { get; set; }
        public string OldName { get; set; }
        public string OldAddress { get; set; }
        [BindProperty]
        public int NewID { get; set; }
        [BindProperty]
        public string NewName { get; set; }
        [BindProperty]
        public string NewAddress { get; set; }
        public Hotel? UpdatedHotel { get; private set; }
        public UpdateModel(IHotelServiceAsync hotelServiceAsync)
        {
            _hotelServiceAsync = hotelServiceAsync;
            OldID = 0;
            OldName = "";
            OldAddress = "";
            NewID = 0;
            NewName = "";
            NewAddress = "";
            UpdatedHotel = null;
        }

        public async Task OnGet(int hotelNo)
        {
            Hotel OldHotel = await _hotelServiceAsync.GetHotelFromIdAsync(hotelNo);
            OldID = OldHotel.HotelNr;
            OldName = OldHotel.Navn;
            OldAddress = OldHotel.Adresse;
            UpdatedHotel = null;
        }

        public async Task OnPost(int oldHotelNo)
        {
            Hotel NewHotel = new Hotel(NewID, NewName, NewAddress);
            bool IsUpdated = await _hotelServiceAsync.UpdateHotelAsync(NewHotel, oldHotelNo);
            if (!IsUpdated)
            {
                Hotel OldHotel = await _hotelServiceAsync.GetHotelFromIdAsync(oldHotelNo);
                OldID = OldHotel.HotelNr;
                OldName = OldHotel.Navn;
                OldAddress = OldHotel.Adresse;
                UpdatedHotel = null;
            }
            else
                UpdatedHotel = NewHotel;
        }
    }
}
