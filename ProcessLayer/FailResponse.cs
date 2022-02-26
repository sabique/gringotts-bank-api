using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ProcessLayer
{
    public class FailResponse : IResponse
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string Message { get; set; }
    }
}
