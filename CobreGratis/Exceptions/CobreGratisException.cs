using System;

namespace BielSystems.Exceptions
{
    public class CobreGratisException : Exception
    {
        public CobreGratisException(string message) : base(message) { }
        public CobreGratisException(string message, Exception innerException) : base(message, innerException) { }

        public override string ToString()
        {
            return string.Format("{0}: {1}", this.GetType().Name, this.Message);
        }
    }

    public class CobreGratisValidationException : CobreGratisException
    {
        public CobreGratisValidationException(string message) : base(message) { }
        public CobreGratisValidationException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisNetworkException : CobreGratisException
    {
        public CobreGratisNetworkException(string message) : base(message) { }
        public CobreGratisNetworkException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisBadRequestException : CobreGratisNetworkException
    {
        public CobreGratisBadRequestException(string message) : base(message) { }
        public CobreGratisBadRequestException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisUnauthorizedException : CobreGratisNetworkException
    {
        public CobreGratisUnauthorizedException(string message) : base(message) { }
        public CobreGratisUnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisForbiddenException : CobreGratisNetworkException
    {
        public CobreGratisForbiddenException(string message) : base(message) { }
        public CobreGratisForbiddenException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisNotFoundException : CobreGratisNetworkException
    {
        public CobreGratisNotFoundException(string message) : base(message) { }
        public CobreGratisNotFoundException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisUnprocessibleEntityException : CobreGratisNetworkException
    {
        public CobreGratisUnprocessibleEntityException(string message) : base(message) { }
        public CobreGratisUnprocessibleEntityException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisServiceUnavailableException : CobreGratisNetworkException
    {
        public CobreGratisServiceUnavailableException(string message) : base(message) { }
        public CobreGratisServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisInternalServerErrorException : CobreGratisNetworkException
    {
        public CobreGratisInternalServerErrorException(string message) : base(message) { }
        public CobreGratisInternalServerErrorException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisTooManyRequestsException : CobreGratisNetworkException
    {
        public CobreGratisTooManyRequestsException(string message) : base(message) { }
        public CobreGratisTooManyRequestsException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisBadGatewayException : CobreGratisNetworkException
    {
        public CobreGratisBadGatewayException(string message) : base(message) { }
        public CobreGratisBadGatewayException(string message, Exception innerException) : base(message, innerException) { }
    }

    public class CobreGratisGatewayTimeoutException : CobreGratisNetworkException
    {
        public CobreGratisGatewayTimeoutException(string message) : base(message) { }
        public CobreGratisGatewayTimeoutException(string message, Exception innerException) : base(message, innerException) { }
    }
}
