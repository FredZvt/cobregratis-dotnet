using BielSystems;
using BielSystems.Cache;
using BielSystems.Exceptions;
using BielSystems.Log;
using BielSystems.Network;
using BielSystems.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTests.Extensions;

namespace UnitTests
{
    public class CobreGratisForTests : CobreGratis
    {
        public CobreGratisForTests(
            string clientAppIdentification, string authToken, ILogger logger, ICache cache,
            INetworkServices networkServices, IXmlSerializer xmlSerializer
            )
            : base(clientAppIdentification, authToken, logger, cache, networkServices, xmlSerializer) { }

        public new IRestServiceResult ExecuteSecureRestService(string httpVerb, string url)
        {
            return base.ExecuteSecureRestService(httpVerb, url);
        }
    }

    [TestClass]
    public class CobreGratisTest
    {
        protected string ClientAppIdentification { get; set; }
        protected string AuthToken { get; set; }
        protected CobreGratisForTests CobreGratis { get; set; }
        protected Mock<ILogger> LoggerMock { get; set; }
        protected Mock<INetworkServices> NetworkServicesMock { get; set; }
        protected Mock<IRestServiceResult> RestServiceResultMock { get; set; }
        protected Mock<IXmlSerializer> XmlSerializerMock { get; set; }
        protected Mock<ICache> CacheMock { get; set; }

        [TestInitialize]
        public void Init()
        {
            this.ClientAppIdentification = "ClientAppIdentification value";
            this.AuthToken = "AuthToken value";
            this.LoggerMock = new Mock<ILogger>();
            this.NetworkServicesMock = new Mock<INetworkServices>();
            this.XmlSerializerMock = new Mock<IXmlSerializer>();
            this.RestServiceResultMock = new Mock<IRestServiceResult>();
            this.CacheMock = new Mock<ICache>();

            this.NetworkServicesMock.Setup(
                ns => ns.ExecuteSecureRestService(
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>()
                )
            ).Returns(this.RestServiceResultMock.Object);

            this.RestServiceResultMock
                .Setup(x => x.Content)
                .Returns(@"<?xml version=""1.0"" encoding=""UTF-8""?><root></root>");

            this.CobreGratis =
                new CobreGratisForTests(
                    this.ClientAppIdentification,
                    this.AuthToken,
                    this.LoggerMock.Object,
                    this.CacheMock.Object,
                    this.NetworkServicesMock.Object,
                    this.XmlSerializerMock.Object
                );
        }

        #region ExecuteSecureRestService Method

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_400_response()
        {
            AssertExt.AssertException<CobreGratisBadRequestException>(
                    () =>
                    {
                        this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(400);
                        this.CobreGratis.ExecuteSecureRestService("", "");
                    },
                    "Invalid request. Probably the content of the request is malformed."
                );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_401_response()
        {
            AssertExt.AssertException<CobreGratisUnauthorizedException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(401);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "The authentication token is invalid."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_403_response()
        {
            AssertExt.AssertException<CobreGratisForbiddenException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(403);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "The contracted plan does not allow access to the API."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_404_response()
        {
            AssertExt.AssertException<CobreGratisNotFoundException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(404);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "The requested resource does not exists."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_429_response()
        {
            AssertExt.AssertException<CobreGratisTooManyRequestsException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(429);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "Too many requests."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_500_response()
        {
            AssertExt.AssertException<CobreGratisInternalServerErrorException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(500);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "There was an internal server error while processing the request."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_502_response()
        {
            AssertExt.AssertException<CobreGratisBadGatewayException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(502);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "Bad gateway."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_504_response()
        {
            AssertExt.AssertException<CobreGratisGatewayTimeoutException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(504);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "Gateway timeout."
               );
        }

        [TestMethod]
        public void ExecuteSecureRestService_should_treat_status_503_response()
        {
            AssertExt.AssertException<CobreGratisServiceUnavailableException>(
                   () =>
                   {
                       this.RestServiceResultMock.Setup(x => x.StatusCode).Returns(503);
                       this.CobreGratis.ExecuteSecureRestService("", "");
                   },
                   "The account has reached some of the limits."
               );
        }

        #endregion
        #region EndPoint Property Validations

        [TestMethod]
        public void EndPoint_property_should_throw_an_exception_if_setted_with_null_string()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.EndPoint = null;
                   },
                   "EndPoint must be a valid url."
               );
        }

        [TestMethod]
        public void EndPoint_property_should_throw_an_exception_if_setted_with_an_invalid_url()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.EndPoint = "I'm an invalid url.";
                   },
                   "EndPoint must be a valid url."
               );
        }

        [TestMethod]
        public void EndPoint_property_should_not_throw_any_exceptions_if_setted_with_a_valid_url()
        {
            this.CobreGratis.EndPoint = "https://app.cobregratis.com.br/";
        }

        [TestMethod]
        public void EndPoint_property_should_append_slash_at_the_end_if_not_present()
        {
            this.CobreGratis.EndPoint = "https://app.cobregratis.com.br";
            Assert.AreEqual("https://app.cobregratis.com.br/", this.CobreGratis.EndPoint);

            this.CobreGratis.EndPoint = "https://app.cobregratis.com.br/";
            Assert.AreEqual("https://app.cobregratis.com.br/", this.CobreGratis.EndPoint);
        }

        #endregion
        #region AuthToken Property Validations

        [TestMethod]
        public void AuthToken_property_should_throw_an_exception_if_setted_with_null_string()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.AuthToken = null;
                   },
                   "AuthToken must be a non empty string."
               );
        }

        [TestMethod]
        public void AuthToken_property_should_throw_an_exception_if_setted_with_an_empty_string()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.AuthToken = string.Empty;
                   },
                   "AuthToken must be a non empty string."
               );
        }

        [TestMethod]
        public void AuthToken_property_should_not_throw_any_exception_if_setted_with_a_non_null_and_non_empty_string()
        {
            this.CobreGratis.AuthToken = "xxxx";
        }

        #endregion
        #region ClientAppIdentification Property Validations

        [TestMethod]
        public void AppIdentification_property_should_throw_an_exception_if_setted_with_null_string()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.ClientAppIdentification = null;
                   },
                   "ClientAppIdentification must be a non empty string."
               );
        }

        [TestMethod]
        public void AppIdentification_property_should_throw_an_exception_if_setted_with_an_empty_string()
        {
            AssertExt.AssertException<CobreGratisValidationException>(
                   () =>
                   {
                       this.CobreGratis.ClientAppIdentification = string.Empty;
                   },
                   "ClientAppIdentification must be a non empty string."
               );
        }

        [TestMethod]
        public void AppIdentification_property_should_not_throw_any_exception_if_setted_with_a_non_null_and_non_empty_string()
        {
            this.CobreGratis.ClientAppIdentification = "xxxx";
        }

        #endregion
        #region GetBankBillets Method

        [TestMethod]
        public void GetBankBillets_should_call_ExecuteSecureRestService_as_expected()
        {
            var billets = this.CobreGratis.GetBankBillets();

            this.NetworkServicesMock.Verify(ns => ns.ExecuteSecureRestService(
                    "GET", "https://app.cobregratis.com.br/bank_billets.xml",
                    "AuthToken value", "X", "application/xml", "ClientAppIdentification value", null
                )
            );
        }

        [TestMethod]
        public void GetBankBillets_should_call_ExecuteSecureRestService_as_expected_when_paged()
        {
            var billets = this.CobreGratis.GetBankBillets(2);

            this.NetworkServicesMock.Verify(ns => ns.ExecuteSecureRestService(
                    "GET", "https://app.cobregratis.com.br/bank_billets.xml?page=2",
                    "AuthToken value", "X", "application/xml", "ClientAppIdentification value", null
                )
            );
        }

        #endregion
    }
}