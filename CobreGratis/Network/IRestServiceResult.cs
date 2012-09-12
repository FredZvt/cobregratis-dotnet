
namespace BielSystems.Network
{
    public interface IRestServiceResult
    {
        string Content { get; set; }
        int StatusCode { get; set; }
    }
}
