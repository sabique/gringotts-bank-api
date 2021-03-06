using AutoMapper;
using DataLayer;
using ServiceModel;
using System;
using System.Threading.Tasks;
using static ServiceModel.Enum;
using static Utility.Constant;

namespace ProcessLayer
{
    public class WithdrawTransactionProcess : ITransactionProcess
    {
        private readonly IAccountData _accountData;
        private readonly ITransactionData _transactionData;
        private readonly IMapper _mapper;
        public WithdrawTransactionProcess(IMapper mapper, IAccountData accountData, ITransactionData transactionData)
        {
            this._mapper = mapper;
            this._accountData = accountData;
            this._transactionData = transactionData;
        }

        public async Task<IResponse> Transact(Transaction transaction)
        {
            try
            {
                var currentBalance = await _accountData.CurrentBalance(transaction.AccountId);

                if (currentBalance < transaction.Amount) throw new Exception("Not enough balance.");

                var transactionDomain = _mapper.Map<DomainModel.Transaction>(transaction);
                transactionDomain.Balance = currentBalance - transactionDomain.Amount;
                transactionDomain.Type = TransactionType.Withdraw.ToString();

                await Task.WhenAll(
                    _transactionData.Transact(transactionDomain),
                    _accountData.UpdateBalance(transactionDomain.AccountId, transactionDomain.Balance)
                    );

                return new SuccessResponse() { Message = $"Transaction is succesfully completed, updated balance is {CurrencySymbol.USD}{transactionDomain.Balance}" };
            }
            catch (Exception e)
            {
                return new FailResponse() { Message = $"Failed to transact - {e.Message}" };
            }
        }
    }
}
