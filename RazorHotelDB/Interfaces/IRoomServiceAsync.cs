using RazorHotelDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorHotelDB.Interfaces
{
    public interface IRoomServiceAsync
    {
        /// <summary>
        /// henter alle værelser til et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomFromHotelAsync(int hotelNr);

        /// <summary>
        /// Henter et specifik værelse fra database 
        /// </summary>
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Den fundne værelse eller null hvis værelset ikke findes</returns>
        Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr);

        /// <summary>
        /// Indsætter et ny værelse i databasen
        /// </summary>
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateRoomAsync(Room room);

        /// <summary>
        /// Opdaterer en værelset i databasen
        /// </summary>
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateRoomAsync(Room room);

        /// <summary>
        /// Sletter et værelse fra databasen
        /// </summary>
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Room> DeleteRoomAsync(int roomNr, int hotelNr);

        public Task<List<Room>> GetRoomsByTypeAsync(string name);
        public Task<List<Room>> GetRoomsByPriceAsync(string name);
        public Task<List<Room>> GetRoomsByHotelAsync(string name);
        public Task<List<Room>> GetAllRoomAsync();

    }
}