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

        public async Task<int> CurrentBalance(int accountId)
        {
            try
            {
                string query = $"SELECT amount FROM public.\"Account\" WHERE id=@accountId;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@accountId", accountId);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                if (table.Rows.Count < 1) throw new Exception("Account does not exist");

                return Convert.ToInt32(table.Rows[0][0].ToString());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateBalance(int accountId, decimal amount)
        {
            try
            {
                string query = $"UPDATE public.\"Account\" SET amount = @amount WHERE id = @accountid;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@amount", amount);
                        myCommand.Parameters.AddWithValue("@accountid", accountId);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
