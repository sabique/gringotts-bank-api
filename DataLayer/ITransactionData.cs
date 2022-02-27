using DomainModel;
using System.Data;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface ITransactionData
    {
        Task<int> Transact(Transaction transaction);
    }
}
