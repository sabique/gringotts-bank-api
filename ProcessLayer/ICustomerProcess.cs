using ServiceModel;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public interface ICustomerProcess
    {
        Task<IResponse> Add(Customer customer);
        Task<Customer> Get(int customerId);
    }
}
