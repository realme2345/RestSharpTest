using System.Net;

namespace RestSharpTest
{
    internal interface IRestResponse
    {
        HttpStatusCode StatusCode { get; }
        string Content { get; }
    }
}