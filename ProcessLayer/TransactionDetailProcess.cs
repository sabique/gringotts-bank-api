using AutoMapper;
using DataLayer;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;
using static Utility.Constant;

namespace ProcessLayer
{
    public class TransactionDetailProcess : ITransactionDetailProcess
    {
        private readonly ITransactionData _transactionData;
        private readonly IMapper _mapper;
        public TransactionDetailProcess(IMapper mapper, ITransactionData transactionData)
        {
            this._mapper = mapper;
            this._transactionData = transactionData;
        }

        public async Task<List<TransactionDetail>> Transactions(int accountId, int skip, int take)
        {
            try
            {
                var response = await _transactionData.TransactionList(accountId, skip, take);

                return _mapper.Map<List<TransactionDetail>>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<TransactionDetail>> Transactions(int accountId, DateTime startDate, DateTime endDate, int skip, int take)
        {
            try
            {
                var response = await _transactionData.TransactionList(accountId, startDate, endDate, skip, take);

                return _mapper.Map<List<TransactionDetail>>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
