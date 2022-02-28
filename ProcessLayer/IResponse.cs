using System.Net;

namespace ProcessLayer
{
    public interface IResponse
    {
        public HttpStatusCode StatusCode { get; }
        public string  Message { get; set; }
    }
}
