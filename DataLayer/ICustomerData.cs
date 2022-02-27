using DomainModel;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ICustomerData
    {
        Task<int> Add(Customer customer);
        Task<DataTable> Get(int customerId);
        Task<bool> Exist(int customerId);
    }
}
