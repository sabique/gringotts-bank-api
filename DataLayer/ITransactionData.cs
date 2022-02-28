using DomainModel;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransactionData
    {
        Task<int> Transact(Transaction transaction);
        Task<List<Transaction>> TransactionList(int accountId, int skip, int take);
    }
}
