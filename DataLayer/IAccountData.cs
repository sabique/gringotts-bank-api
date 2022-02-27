using DomainModel;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IAccountData
    {
        Task<int> Add(Account account);
    }
}
