using System.Net;

namespace ProcessLayer
{
    public class FailResponse : IResponse
    {
        public HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        public string Message { get; set; }
    }
}
