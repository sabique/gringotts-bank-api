using DomainModel;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Utility;

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

        public async Task<List<Transaction>> TransactionList(int accountId, int skip, int take)
        {
            try
            {
                string query = $"SELECT id AS \"Id\", amount AS \"Amount\", type AS \"Type\", balance AS \"Balance\", createdon AS \"CreatedOn\", accountid AS \"AccountId\" FROM public.\"Transaction\" WHERE accountid=@accountid ORDER BY id desc LIMIT @take OFFSET @skip;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@accountid", accountId);
                        myCommand.Parameters.AddWithValue("@skip", skip);
                        myCommand.Parameters.AddWithValue("@take", take);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
                    }
                }

                return table.GetTCollection<Transaction>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Transaction>> TransactionList(int accountId, DateTime startDate, DateTime endDate, int skip, int take)
        {
            try
            {
                string query = $"SELECT id AS \"Id\", amount AS \"Amount\", type AS \"Type\", balance AS \"Balance\", createdon AS \"CreatedOn\", accountid AS \"AccountId\" FROM public.\"Transaction\" WHERE accountid = @accountId AND createdon >= @startDate AND createdon <= @endDate ORDER BY id desc LIMIT @take OFFSET @skip;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@accountid", accountId);
                        myCommand.Parameters.AddWithValue("@startDate", startDate);
                        myCommand.Parameters.AddWithValue("@endDate", endDate);
                        myCommand.Parameters.AddWithValue("@skip", skip);
                        myCommand.Parameters.AddWithValue("@take", take);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
                    }
                }

                return table.GetTCollection<Transaction>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
