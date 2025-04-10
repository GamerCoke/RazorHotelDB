using RazorHotelDB.Interfaces;
using Microsoft.Data.SqlClient;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class RoomService : IRoomServiceAsync
    {
        private string connectionString = Secret.ConnectionString;
        private string queryString = "SELECT Hotel_No, Room_No, Types, Price FROM Room";
        public async Task<bool> CreateRoomAsync(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string insertString = "Insert Into Room Values (@RNR, @HNR, @TYPE, @PRICE)";
                SqlCommand insertQuery = new SqlCommand(insertString, connection);
                try
                {
                    insertQuery.Parameters.AddWithValue("@HNR", room.HotelNr);
                    insertQuery.Parameters.AddWithValue("@RNR", room.VærelseNr);
                    insertQuery.Parameters.AddWithValue("@TYPE", room.VærelseType);
                    insertQuery.Parameters.AddWithValue("@PRICE", room.Pris);
                    await connection.OpenAsync();
                    int noOfRows = await insertQuery.ExecuteNonQueryAsync();
                    if (noOfRows == -1)
                        return false;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            Room room;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string deleteString = "DELETE from Room WHERE Hotel_No = @HNR AND Room_No = @RNR";
                SqlCommand deleteQuery = new SqlCommand(deleteString, connection);
                try
                {
                    room = await GetRoomFromIdAsync(roomNr, hotelNr);
                    deleteQuery.Parameters.AddWithValue("@HNR", hotelNr);
                    deleteQuery.Parameters.AddWithValue("@RNR", roomNr);
                    await deleteQuery.Connection.OpenAsync();
                    SqlDataReader reader;
                    int noOfRows = await deleteQuery.ExecuteNonQueryAsync();
                    if (noOfRows == -1)
                        return null;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
                return room;
            }
        }
        public async Task<List<Room>> GetAllRoomFromHotelAsync(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE Hotel_no LIKE {hotelNr}", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(hotelNr, roomNr, roomType, roomPrice);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int roomNr = reader.GetInt32(0);
                        int hotelNr = reader.GetInt32(1);
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(hotelNr, roomNr, roomType, roomPrice);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return rooms;
        }

        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE Room_No = {roomNr} AND Hotel_no = {hotelNr}", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    //Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    { 
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(roomNr, hotelNr, roomType, roomPrice);
                        reader.Close();
                        return room;
                    }
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return null;
        }

        public async Task<List<Room>> GetRoomsByTypeAsync(string name)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE types LIKE %{name}%", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(1);
                        int roomNr = reader.GetInt32(0);
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(hotelNr, roomNr, roomType, roomPrice);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetRoomsByPriceAsync(string name)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE types LIKE %{name}%", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(1);
                        int roomNr = reader.GetInt32(0);
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(hotelNr, roomNr, roomType, roomPrice);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return rooms;
        }

        public async Task<List<Room>> GetRoomsByHotelAsync(string name)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE types LIKE %{name}%", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(1);
                        int roomNr = reader.GetInt32(0);
                        string roomType = reader.GetString(2);
                        double roomPrice = reader.GetDouble(3);
                        Room room = new Room(hotelNr, roomNr, roomType, roomPrice);
                        rooms.Add(room);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
            }
            return rooms;
        }

        public async Task<bool> UpdateRoomAsync(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    const string updateString = "UPDATE Room SET Types = @TYPE, Price = @PRICE WHERE Hotel_No = @HNR AND Room_No = @RNR";
                    SqlCommand command = new SqlCommand(updateString, connection);
                    command.Parameters.AddWithValue("@TYPE", room.VærelseType);
                    command.Parameters.AddWithValue("@PRICE", room.Pris);
                    command.Parameters.AddWithValue("@HNR", room.HotelNr);
                    command.Parameters.AddWithValue("@RNR", room.VærelseNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    reader.Read();
                    reader.Close();
                    return true;
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                }
                return false;
            }
        }
    }
}
