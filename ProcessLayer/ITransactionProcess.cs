using ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public interface ITransactionProcess
    {
        Task<IResponse> Transact(Transaction transaction);
    }
}
