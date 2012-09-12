using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using BielSystems.Cache;
using BielSystems.Exceptions;
using BielSystems.Log;
using BielSystems.Network;
using BielSystems.Serialization;

namespace BielSystems
{
    public class CobreGratis : ICobreGratis
    {
        #region Construction

        internal CobreGratis(
            string clientAppIdentification,
            string authToken,
            ILogger logger,
            ICache cache,
            INetworkServices networkServices,
            IXmlSerializer xmlSerializer
            )
        {
            Initialize(clientAppIdentification, authToken, logger, cache, networkServices, xmlSerializer);
        }

        public CobreGratis(
            string clientAppIdentification,
            string authToken,
            ILogger logger = null,
            ICache cache = null
            )
        {
            Initialize(clientAppIdentification, authToken, logger, cache);
        }

        protected void Initialize(
            string clientAppIdentification,
            string authToken,
            ILogger logger,
            ICache cache = null,
            INetworkServices networkServices = null,
            IXmlSerializer xmlSerializer = null
            )
        {
            this.EndPoint = @"https://app.cobregratis.com.br/";
            this.AuthToken = authToken;
            this.ClientAppIdentification = clientAppIdentification;
            this.Logger = new LogHelper(logger);
            this.Cache = cache ?? new StaticCache();
            this.NetworkServices = networkServices ?? new NetworkServices(this.Logger, this.Cache);
            this.XmlSerializer = xmlSerializer ?? new XmlSerializer(this.Logger);
        }

        #endregion

        protected LogHelper Logger { get; set; }
        protected INetworkServices NetworkServices { get; set; }
        protected IXmlSerializer XmlSerializer { get; set; }
        protected ICache Cache { get; set; }

        /// <summary>
        /// Enable HTTP cache.
        /// Default value is true.
        /// </summary>
        public bool EnableCache
        {
            get { return this.NetworkServices.EnableCache; }
            set { this.NetworkServices.EnableCache = value; }
        }

        /// <summary>
        /// EndPoint of the REST API.
        /// Default value is https://app.cobregratis.com.br/
        /// </summary>
        public string EndPoint
        {
            get { return this.endPoint; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CobreGratisValidationException("EndPoint must be a valid url.");

                try
                {
                    var uri = new Uri(value);
                }
                catch (Exception ex)
                {
                    throw new CobreGratisValidationException("EndPoint must be a valid url.", ex);
                    throw;
                }

                if (!value.EndsWith("/"))
                {
                    this.endPoint = value + "/";
                }
                else
                {
                    this.endPoint = value;
                }
            }
        }
        protected string endPoint;

        /// <summary>
        /// Authentication token given by CobreGratis to your account.
        /// </summary>
        public string AuthToken
        {
            get { return this.authToken; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CobreGratisValidationException("AuthToken must be a non empty string.");

                this.authToken = value;
            }
        }
        protected string authToken;

        /// <summary>
        /// The text that identifies your account to CobreGratis.
        /// Value sent in User-Agent HTTP header.
        /// </summary>
        public string ClientAppIdentification
        {
            get { return this.clientAppIdentification; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new CobreGratisValidationException("ClientAppIdentification must be a non empty string.");

                this.clientAppIdentification = value;
            }
        }
        protected string clientAppIdentification;

        protected IRestServiceResult ExecuteSecureRestService(string httpVerb, string url, string requestContent = null)
        {
            IRestServiceResult response;

            try
            {
                response = NetworkServices.ExecuteSecureRestService(httpVerb, url, AuthToken, "X", "application/xml", ClientAppIdentification, requestContent);
            }
            catch (Exception ex)
            {
                throw new CobreGratisNetworkException("Unexpected network error.", ex);
            }

            switch (response.StatusCode)
            {
                case 400: throw new CobreGratisBadRequestException("Invalid request. Probably the content of the request is malformed.");
                case 401: throw new CobreGratisUnauthorizedException("The authentication token is invalid.");
                case 403: throw new CobreGratisForbiddenException("The contracted plan does not allow access to the API.");
                case 404: throw new CobreGratisNotFoundException("The requested resource does not exists.");
                case 422: throw new CobreGratisUnprocessibleEntityException("The request could not be processed.");
                case 429: throw new CobreGratisTooManyRequestsException("Too many requests.");
                case 500: throw new CobreGratisInternalServerErrorException("There was an internal server error while processing the request.");
                case 502: throw new CobreGratisBadGatewayException("Bad gateway.");
                case 503: throw new CobreGratisServiceUnavailableException("The account has reached some of the limits.");
                case 504: throw new CobreGratisGatewayTimeoutException("Gateway timeout.");
            }

            return response;
        }

        protected List<BankBillet> DeserializeBankBillets(IRestServiceResult response)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(response.Content);
            var nodes = xmlDoc.SelectNodes("//bank-billet");

            var billets = new List<BankBillet>();
            foreach (XmlNode node in nodes)
            {
                var billet = XmlSerializer.DeserializeXmlNodeToObject<BankBillet>(node);
                billets.Add(billet);
            }

            return billets;
        }

        protected BankBillet DeserializeSingleBankBillet(IRestServiceResult response)
        {
            var billets = DeserializeBankBillets(response);
            return billets.FirstOrDefault();
        }

        public IEnumerable<BankBillet> GetBankBillets(int? page = null)
        {
            var url = EndPoint + "bank_billets.xml" + (page.HasValue ? "?page=" + page.Value.ToString() : string.Empty);
            var response = ExecuteSecureRestService("GET", url);
            var billets = DeserializeBankBillets(response);

            return billets;
        }

        public BankBillet GetBankBillet(int billetId)
        {
            var url = EndPoint + string.Format("bank_billets/{0}.xml", billetId);
            var response = ExecuteSecureRestService("GET", url);
            var billet = DeserializeSingleBankBillet(response);

            return billet;
        }

        public void DeleteBillet(int billetId)
        {
            var url = EndPoint + string.Format("bank_billets/{0}.xml", billetId);
            var response = ExecuteSecureRestService("DELETE", url);
        }

        public BankBillet CreateBankBillet(
            int accountId,
            decimal amount,
            DateTime expireAt,
            string name,
            string description = null,
            string instructions = null,
            string cnpjCpf = null,
            string address = null,
            string zipcode = null,
            string neighborhood = null,
            string city = null,
            string state = null,
            string documentNumber = null,
            decimal? documentAmount = null,
            decimal? discountAmount = null,
            decimal? percentFines = null,
            decimal? percentInterestDay = null,
            string comments = null
            )
        {
            var requestContent =
                XmlSerializer.SerializeForCreateOrUpdateBankBillet(
                    accountId, amount, expireAt, name, description, instructions, cnpjCpf, address, 
                    zipcode, neighborhood, city, state, documentNumber, documentAmount, 
                    discountAmount, percentFines, percentInterestDay, comments
                );

            var url = EndPoint + string.Format("bank_billets.xml");
            var response = ExecuteSecureRestService("POST", url, requestContent);
            var responseBillet = DeserializeSingleBankBillet(response);

            return responseBillet;
        }

        public void UpdateBankBillet(
            int billetId,
            int? accountId = null,
            decimal? amount = null,
            DateTime? expireAt = null,
            string name = null,
            string description = null,
            string instructions = null,
            string cnpjCpf = null,
            string address = null,
            string zipcode = null,
            string neighborhood = null,
            string city = null,
            string state = null,
            string documentNumber = null,
            decimal? documentAmount = null,
            decimal? discountAmount = null,
            decimal? percentFines = null,
            decimal? percentInterestDay = null,
            string comments = null
            )
        {
            var requestContent =
                XmlSerializer.SerializeForCreateOrUpdateBankBillet(
                    accountId, amount, expireAt, name, description, instructions, cnpjCpf, address,
                    zipcode, neighborhood, city, state, documentNumber, documentAmount,
                    discountAmount, percentFines, percentInterestDay, comments
                );

            var url = EndPoint + string.Format("bank_billets/{0}.xml", billetId);
            var response = ExecuteSecureRestService("PUT", url, requestContent);
        }
    }
}
