
namespace BielSystems.Network
{
    public class RestServiceResult : IRestServiceResult
    {
        public string Content { get; set; }
        public int StatusCode { get; set; }
    }
}
