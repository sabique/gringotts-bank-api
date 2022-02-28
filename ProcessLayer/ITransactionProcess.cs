using ServiceModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public interface ITransactionDetailProcess
    {
        Task<List<TransactionDetail>> Transactions(int accountId, int skip, int take);
        Task<List<TransactionDetail>> Transactions(int accountId, DateTime startDate, DateTime endDate, int skip, int take);
    }
}
