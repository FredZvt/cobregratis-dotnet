using System;
using System.Xml;

namespace BielSystems.Serialization
{
    public interface IXmlSerializer
    {
        T DeserializeXmlNodeToObject<T>(XmlNode xml);

        string SerializeForCreateOrUpdateBankBillet(
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
            );
    }
}
