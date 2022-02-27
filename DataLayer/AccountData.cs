using DomainModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AccountData : IAccountData
    {
        private readonly IConfiguration _configuration;
        public AccountData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Add(Account account)
        {
            try
            {
                string query = $"INSERT INTO public.\"Account\" (type, amount, currency, createdon, modifiedon, customerid) values (@type,@amount, @currency,@createdon, @modifiedon, @customerid) RETURNING id;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@type", account.Type);
                        myCommand.Parameters.AddWithValue("@amount", account.Amount);
                        myCommand.Parameters.AddWithValue("@currency", account.Currency);
                        myCommand.Parameters.AddWithValue("@createdon", account.CreatedOn);
                        myCommand.Parameters.AddWithValue("@modifiedon", account.ModifiedOn);
                        myCommand.Parameters.AddWithValue("@customerid", account.CustomerId);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return Convert.ToInt32(table.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
