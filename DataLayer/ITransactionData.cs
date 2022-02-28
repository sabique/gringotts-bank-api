using DomainModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransactionData
    {
        Task<int> Transact(Transaction transaction);
        Task<List<Transaction>> TransactionList(int accountId, int skip, int take);
        Task<List<Transaction>> TransactionList(int accountId, DateTime startDate, DateTime endDate, int skip, int take);
    }
}
