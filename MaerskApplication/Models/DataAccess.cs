using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models
{
    public class DataAccess
    {
        private string _connectionString;

        public DataAccess()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public List<Users> GetUser()
        {
            using(SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<Users> userList = new List<Users>();

                string sql = "SELECT * FROM Users";

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users user = new Users
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Name = reader["Name"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    };
                    userList.Add(user);
                }

                return userList;
            }
        }


        public List<Users> GetAgents()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<Users> userList = new List<Users>();

                string sql = "SELECT * FROM Users WHERE Role = 0";

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Users user = new Users
                    {
                        Username = reader["Username"].ToString(),
                        Password = reader["Password"].ToString(),
                        Name = reader["Name"].ToString(),
                        Role = Convert.ToInt32(reader["Role"])
                    };
                    userList.Add(user);
                }

                return userList;
            }
        }

        public List<Customer> GetCustomer()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<Customer> customerList = new List<Customer>();

                string sql = "SELECT * FROM Customer";

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        CompanyName = reader["CompanyName"].ToString()
                    };
                    customerList.Add(customer);
                }

                return customerList;
            }
        }

        public Customer GetCustomer(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<Customer> customerList = new List<Customer>();

                string sql = "SELECT * FROM Customer WHERE Id = " + id;

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Customer customer = new Customer
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        CompanyName = reader["CompanyName"].ToString()
                    };
                    customerList.Add(customer);
                }

                return customerList.First();
            }
        }

        public void AddCustomer(Customer customer)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string sql = "INSERT INTO Customer VALUES(@name, @company)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@name", customer.Name);
                cmd.Parameters.AddWithValue("@company", customer.CompanyName);

                cmd.ExecuteNonQuery();
            }
        }


        public void AddAgent(Users agent)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string sql = "INSERT INTO Users VALUES(@username, @password,  @role, @name)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@username", agent.Username);
                cmd.Parameters.AddWithValue("@password", agent.Password);
                cmd.Parameters.AddWithValue("@name", agent.Name);
                cmd.Parameters.AddWithValue("@role", agent.Role);

                cmd.ExecuteNonQuery();
            }
        }


        public void AddShipping(ShippingDetail shipping)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string sql = "INSERT INTO ShippingDetail VALUES(@slots, @dateFrom, @dateTo, @source, @destination)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@slots", shipping.AvailableSlots);
                cmd.Parameters.AddWithValue("@dateFrom", shipping.DateFrom);
                cmd.Parameters.AddWithValue("@dateTo", shipping.DateTo);
                cmd.Parameters.AddWithValue("@source", shipping.Source);
                cmd.Parameters.AddWithValue("@destination", shipping.Destination);

                cmd.ExecuteNonQuery();
            }
        }


        public void AddBooking(Booking booking)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                string sql = "INSERT INTO Booking VALUES(@custId, @shipId, @slots)";

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@custId", booking.CustomerId);
                cmd.Parameters.AddWithValue("@shipId", booking.ShippingId);
                cmd.Parameters.AddWithValue("@slots", booking.Slots);

                cmd.ExecuteNonQuery();

                var shipping = GetShippingDetail(booking.ShippingId);

                sql = "UPDATE ShippingDetail SET AvailableSlots = @slots WHERE Id = @id";

                SqlCommand cmd2 = new SqlCommand(sql, con);
                cmd2.Parameters.AddWithValue("@slots", shipping.AvailableSlots - booking.Slots);
                cmd2.Parameters.AddWithValue("@id", shipping.Id);

                cmd2.ExecuteNonQuery();
            }
        }

        public List<ShippingDetail> GetShippingDetail()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<ShippingDetail> shippingList = new List<ShippingDetail>();

                string sql = "SELECT * FROM ShippingDetail";

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ShippingDetail shipping = new ShippingDetail
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                        DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                        AvailableSlots = Convert.ToInt32(reader["AvailableSlots"]),
                        Source = reader["Source"].ToString(),
                        Destination = reader["Destination"].ToString()
                    };
                    shippingList.Add(shipping);
                }

                return shippingList;
            }
        }

        public ShippingDetail GetShippingDetail(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<ShippingDetail> shippingList = new List<ShippingDetail>();

                string sql = "SELECT * FROM ShippingDetail WHERE Id = " + id;

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ShippingDetail shipping = new ShippingDetail
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DateFrom = reader.GetDateTime(reader.GetOrdinal("DateFrom")),
                        DateTo = reader.GetDateTime(reader.GetOrdinal("DateTo")),
                        AvailableSlots = Convert.ToInt32(reader["AvailableSlots"]),
                        Source = reader["Source"].ToString(),
                        Destination = reader["Destination"].ToString()
                    };
                    shippingList.Add(shipping);
                }

                return shippingList.First();
            }
        }

        public List<Booking> GetBooking()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                List<Booking> bookingList = new List<Booking>();

                string sql = "SELECT * FROM Booking";

                SqlCommand cmd = new SqlCommand(sql, con);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Booking booking = new Booking
                    {
                        ShippingDetail = GetShippingDetail(Convert.ToInt32(reader["ShippingId"])),
                        Customer = GetCustomer(Convert.ToInt32(reader["CustomerId"])),
                        Slots = Convert.ToInt32(reader["Slots"])
                    };

                    booking.ShippingId = booking.ShippingDetail.Id;
                    booking.CustomerId = booking.Customer.Id;

                    bookingList.Add(booking);
                }

                return bookingList;
            }
        }
    }
}