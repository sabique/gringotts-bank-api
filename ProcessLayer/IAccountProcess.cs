using ServiceModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public interface IAccountProcess
    {
        Task<IResponse> Add(OpenAccount account);
        Task<List<AccountDetail>> GetCustomerAccounts(int customerId, int skip, int take);
        Task<AccountDetail> Get(int accountId);
    }
}
