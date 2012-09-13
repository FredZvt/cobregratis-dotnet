using System;
using System.IO;
using System.Net;
using System.Text;
using BielSystems.Cache;
using BielSystems.Log;

namespace BielSystems.Network
{
    public class NetworkServices : INetworkServices
    {
        protected LogHelper Logger { get; set; }
        protected ICache Cache { get; set; }
        public Boolean EnableCache { get; set; }

        public NetworkServices(LogHelper logger, ICache cache)
        {
            this.Logger = logger;
            this.Cache = cache;
            this.EnableCache = true;
        }

        protected void LogRequest(HttpWebRequest request, string content)
        {
            Logger.Log("--- Request log start ---");
            Logger.Log("New HTTP request: {0}", DateTime.Now);
            Logger.Log("Method: {0}", request.Method);
            Logger.Log("Url: {0}", request.RequestUri);
            
            Logger.Log("Headers:");
            foreach (var key in request.Headers.AllKeys)
                Logger.Log("[{0}] = {1}", key, request.Headers[key]);

            Logger.Log("Body:");
            Logger.Log(content);

            Logger.Log("--- Request log end ---");
        }

        protected void LogResponse(HttpWebResponse response, string content)
        {
            Logger.Log("--- Response log start ---");
            Logger.Log("New HTTP response: {0}", DateTime.Now);
            Logger.Log("StatusCode: {0} {1}", (int)response.StatusCode, response.StatusDescription);

            Logger.Log("Headers:");
            foreach (var key in response.Headers.AllKeys)
                Logger.Log("[{0}] = {1}", key, response.Headers[key]);

            Logger.Log("Body:");
            Logger.Log(content);

            Logger.Log("--- Response log end ---");
        }

        public IRestServiceResult ExecuteSecureRestService(
            string httpVerb,
            string url,
            string username,
            string password,
            string contentType,
            string userAgent,
            string requestContent = null
            )
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            var credentialCache = new CredentialCache();
            credentialCache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Credentials = credentialCache;
            request.PreAuthenticate = true;
            request.Method = httpVerb;
            request.ContentType = contentType;
            request.UserAgent = userAgent;
            request.KeepAlive = false;

            WriteRequestContent(request, requestContent);

            var cacheETagKey = string.Format("ExecuteSecureRestService_etag_{0}_{1}", username, url);
            var cacheContentKey = string.Format("ExecuteSecureRestService_content_{0}_{1}", username, url);

            if (request.Method == "GET")
                DoCacheStuffForRequest(request, cacheETagKey, cacheContentKey);

            LogRequest(request, requestContent);

            // http://fearthecowboy.com/2011/09/02/fixing-webrequests-desire-to-throw-exceptions-instead-of-returning-status/
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    response = (HttpWebResponse)wex.Response;
                }
                else
                {
                    throw;
                }
            }

            string responseContent = null;
            RestServiceResult result = null;
            if (response.StatusCode == HttpStatusCode.NotModified)
            {
                responseContent = (string)Cache.LoadData(cacheContentKey);

                result = new RestServiceResult()
                {
                    Content = responseContent,
                    StatusCode = (int)response.StatusCode
                };
            }
            else
            {
                responseContent = ReadResponseContent(response);

                if (request.Method == "GET")
                    DoCacheStuffForResponse(response, cacheETagKey, cacheContentKey, responseContent);
            }

            result = new RestServiceResult()
            {
                Content = responseContent,
                StatusCode = (int)response.StatusCode
            };

            LogResponse(response, result.Content);
            response.Close();

            return result;
        }

        protected void DoCacheStuffForRequest(HttpWebRequest request, string cacheETagKey, string cacheContentKey)
        {
            if (EnableCache && Cache != null)
            {
                if (Cache.ContainsKey(cacheETagKey) && Cache.ContainsKey(cacheContentKey))
                {
                    request.Headers.Add("If-None-Match", (string)Cache.LoadData(cacheETagKey));
                }
            }
        }

        protected void DoCacheStuffForResponse(HttpWebResponse response, string cacheETagKey, string cacheContentKey, string content)
        {
            if (EnableCache && Cache != null)
            {
                var eTagValue = response.Headers["ETag"];
                if (!string.IsNullOrWhiteSpace(eTagValue))
                {
                    Cache.StoreData(cacheETagKey, eTagValue);
                    Cache.StoreData(cacheContentKey, content);
                }
            }
        }

        protected void WriteRequestContent(HttpWebRequest request, string content)
        {
            if (content != null)
            {
                var bytes = Encoding.UTF8.GetBytes(content);
                request.ContentLength = bytes.Length;


                var reqStream = request.GetRequestStream();
                reqStream.Write(bytes, 0, bytes.Length);
                reqStream.Close();
            }
        }

        protected string ReadResponseContent(HttpWebResponse response)
        {
            var respStream = response.GetResponseStream();
            var reader = new StreamReader(respStream, System.Text.Encoding.UTF8);
            var content = reader.ReadToEnd();
            reader.Close();

            return content;
        }
    }
}
