using Microsoft.Data.SqlClient;
using RazorHotelDB.Interfaces;
using RazorHotelDB.Models;

namespace RazorHotelDB.Services
{
    public class HotelService: IHotelServiceAsync
    {
        private string connectionString = Secret.ConnectionString;
        private string queryString = "SELECT Hotel_No, Name, Address FROM Hotel";

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string insertString = "Insert Into Hotel Values (@NR, @NAME, @ADDRESS)";
                SqlCommand insertQuery = new SqlCommand(insertString, connection);
                try
                {
                    insertQuery.Parameters.AddWithValue("@NR", hotel.HotelNr);
                    insertQuery.Parameters.AddWithValue("@NAME", hotel.Navn);
                    insertQuery.Parameters.AddWithValue("@ADDRESS", hotel.Adresse);
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

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel hotel;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string deleteString = $"DELETE FROM Hotel WHERE Hotel_No = {hotelNr}";
                SqlCommand deleteQuery = new SqlCommand(deleteString, connection);
                try
                {
                    hotel = await GetHotelFromIdAsync(hotelNr);
                    await connection.OpenAsync();
                    int noOfRows = await deleteQuery.ExecuteNonQueryAsync();
                    if (noOfRows == -1)
                        return null;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
            return hotel;
        }

        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hoteller = new List<Hotel>();
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
                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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
            return hoteller;
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE Hotel_No = {hotelNr}", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(1000);
                    while (await reader.ReadAsync())
                    {
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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
            return hoteller.First();
        }

        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString + $" WHERE Name like '%{name}%'", connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    Thread.Sleep(500);
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);
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
            return hoteller;
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                const string updateString = "Update Hotel Set Name = @NAME, Address = @ADDRESS where Hotel_No = @NR";
                SqlCommand updateQuery = new SqlCommand(updateString, connection);
                try
                {
                    updateQuery.Parameters.AddWithValue("@NR", hotelNr);
                    updateQuery.Parameters.AddWithValue("@NAME", hotel.Navn);
                    updateQuery.Parameters.AddWithValue("@ADDRESS", hotel.Adresse);
                    connection.Open();
                    int noOfRows = await updateQuery.ExecuteNonQueryAsync();
                    return noOfRows > 0;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return false;
        }
    }
}
