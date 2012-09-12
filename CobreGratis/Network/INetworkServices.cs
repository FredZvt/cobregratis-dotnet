
using System;
namespace BielSystems.Network
{
    public interface INetworkServices
    {
        Boolean EnableCache { get; set; }
        IRestServiceResult ExecuteSecureRestService(string httpVerb, string url, string username, string password, string contentType, string userAgent, string requestContent = null);
    }
}
