using DomainModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public class CustomerData : ICustomerData
    {
        private readonly IConfiguration _configuration;
        public CustomerData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<int> Add(Customer customer)
        {
            try
            {
                string query = $"INSERT INTO public.\"Customer\" (firstname, lastname, email) values (@firstname, @lastname, @email) RETURNING id;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@firstname", customer.FirstName);
                        myCommand.Parameters.AddWithValue("@lastname", customer.LastName);
                        myCommand.Parameters.AddWithValue("@email", customer.Email);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return Convert.ToInt32(table.Rows[0][0].ToString());
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
