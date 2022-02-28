using System.Net;

namespace ProcessLayer
{
    public class SuccessResponse : IResponse
    {
        public HttpStatusCode StatusCode { get => HttpStatusCode.OK; }
        public string Message { get; set; }
    }
}
