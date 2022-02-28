using ServiceModel;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public interface ITransactionProcess
    {
        Task<IResponse> Transact(Transaction transaction);
    }
}
