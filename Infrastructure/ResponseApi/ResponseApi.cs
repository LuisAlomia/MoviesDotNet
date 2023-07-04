using System.Net;

namespace Infrastructure.ResponseApi
{
    public class ResponseApi
    {
        public HttpStatusCode statusCode { get; set; }
        public bool isSuccessful { get; set; } = true;
        public List<string> errorMessage { get; set; }
        public object result { get; set; }
    }
}
