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

        public async Task<int> CurrentBalance(long accountId)
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

        public async Task UpdateBalance(long accountId, double amount)
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

        public async Task<List<Account>> AccountList(int customerId, int skip, int take)
        {
            try
            {
                string query = $"SELECT id AS \"Id\", amount AS \"Amount\", type AS \"Type\", currency AS \"Currency\", createdon AS \"CreatedOn\" FROM public.\"Account\" WHERE customerid=@customerid ORDER BY id asc LIMIT @take OFFSET @skip;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@customerid", customerId);
                        myCommand.Parameters.AddWithValue("@skip", skip);
                        myCommand.Parameters.AddWithValue("@take", take);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
                    }
                }

                return table.GetTCollection<Account>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Account> Get(int accountId)
        {
            try
            {
                string query = $"SELECT id AS \"Id\", amount AS \"Amount\", type AS \"Type\", currency AS \"Currency\", createdon AS \"CreatedOn\" FROM public.\"Account\" WHERE id=@accountid;";

                DataTable table = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("database");

                NpgsqlDataReader myReader;
                using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
                {
                    await myCon.OpenAsync();
                    using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                    {

                        myCommand.Parameters.AddWithValue("@accountid", accountId);
                        myReader = await myCommand.ExecuteReaderAsync();
                        table.Load(myReader);
                        myReader.Close();
                        await myCon.CloseAsync();
                    }
                }

                return table.GetT<Account>();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
