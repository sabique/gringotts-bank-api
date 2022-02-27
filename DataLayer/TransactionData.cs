using DomainModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TransactionData : ITransactionData
    {
        private readonly IConfiguration _configuration;
        public TransactionData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<int> Transact(Transaction transaction)
        {
            try
            {
                string query = $"INSERT INTO public.\"Transaction\" (type, amount, createdon, accountid, balance) values (@type, @amount, @createdon, @accountid, @balance) RETURNING id;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@type", transaction.Type);
                        myCommand.Parameters.AddWithValue("@amount", transaction.Amount);
                        myCommand.Parameters.AddWithValue("@createdon", transaction.CreatedOn);
                        myCommand.Parameters.AddWithValue("@accountid", transaction.AccountId);
                        myCommand.Parameters.AddWithValue("@balance", transaction.Balance);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
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
