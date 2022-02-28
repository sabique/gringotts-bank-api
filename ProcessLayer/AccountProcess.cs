using AutoMapper;
using DataLayer;
using ServiceModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Utility;

namespace ProcessLayer
{
    public class AccountProcess : IAccountProcess
    {
        private readonly IAccountData _accountData;
        private readonly ICustomerData _customerData;
        private readonly IMapper _mapper;
        public AccountProcess(IMapper mapper, IAccountData accountData, ICustomerData customerData)
        {
            this._mapper = mapper;
            this._accountData = accountData;
            _customerData = customerData;
        }

        public async Task<IResponse> Add(OpenAccount account)
        {
            try
            {
                if (!(await _customerData.Exist(account.CustomerId)))
                    throw new Exception("Invalid Customer Id");

                var accountDomain = _mapper.Map<DomainModel.Account>(account);

                var response = await _accountData.Add(accountDomain);

                return new SuccessResponse() { Message = $"Opened new account with ID {response}" };
            }
            catch (Exception e)
            {
                return new FailResponse() { Message = $"Failed to open new account - {e.Message}" };
            }
        }

        public async Task<List<AccountDetail>> GetCustomerAccounts(int customerId, int skip, int take)
        {
            try
            {
                var response = await _accountData.AccountList(customerId, skip, take);

                return _mapper.Map<List<AccountDetail>>(response);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
