using System;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using BielSystems.Log;

namespace BielSystems.Serialization
{
    public class XmlSerializer : IXmlSerializer
    {
        protected LogHelper Logger { get; set; }

        public XmlSerializer(LogHelper logger)
	    {
            this.Logger = logger;
	    }
        
        public T DeserializeXmlNodeToObject<T>(XmlNode xml)
        {
            var obj = Activator.CreateInstance<T>();
            var objProps = typeof(T).GetProperties();

            var regex = @"([A-Z])([A-Z][a-z])|([a-z0-9])([A-Z])";
            var replaceRule = "$1$3-$2$4";

            var ni = new NumberFormatInfo();
            ni.CurrencyDecimalSeparator = ".";

            foreach (PropertyInfo prop in objProps)
            {
                var xmlNodeName = Regex.Replace(prop.Name, regex, replaceRule).ToLower();
                var xmlNode = xml.SelectSingleNode(xmlNodeName);

                if (xmlNode != null)
                {
                    var nilAttr = xmlNode.Attributes["nil"];
                    if (nilAttr == null || nilAttr.Value != "true")
                    {
                        var strValue = xmlNode.InnerText;
                        var type = prop.PropertyType;
                        object value = null;

                        if (type == typeof(string))
                        {
                            value = strValue;
                        }
                        else
                        {
                            if (type.IsGenericType &&
                                type.GetGenericTypeDefinition() == typeof(Nullable<>) &&
                                String.IsNullOrEmpty(strValue) == false
                                )
                            {
                                value = Convert.ChangeType(strValue, type.GetGenericArguments()[0], ni);
                            }
                            else
                            {
                                value = Convert.ChangeType(strValue, type, ni);
                            }
                        }

                        prop.SetValue(obj, value, null);
                    }
                }
            }

            return (T)obj;
        }

        public string SerializeForCreateOrUpdateBankBillet(
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
            var ni = new NumberFormatInfo();
            ni.CurrencyDecimalSeparator = ".";

            var requestContent = new StringBuilder();
            requestContent.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
            requestContent.Append("<bank-billet>");

            if (amount != null) requestContent.AppendFormat("<amount>{0}</amount>", amount.Value.ToString("0.00", ni));
            if (expireAt != null) requestContent.AppendFormat("<expire-at>{0}</expire-at>", expireAt.Value.ToString("yyyy-MM-dd"));
            if (name != null) requestContent.AppendFormat("<name>{0}</name>", name);
            if (description != null) requestContent.AppendFormat("<description>{0}</description>", description);
            if (instructions != null) requestContent.AppendFormat("<instructions>{0}</instructions>", instructions);
            if (cnpjCpf != null) requestContent.AppendFormat("<cnpj-cpf>{0}</cnpj-cpf>", cnpjCpf);
            if (address != null) requestContent.AppendFormat("<address>{0}</address>", address);
            if (zipcode != null) requestContent.AppendFormat("<zipcode>{0}</zipcode>", zipcode);
            if (neighborhood != null) requestContent.AppendFormat("<neighborhood>{0}</neighborhood>", neighborhood);
            if (city != null) requestContent.AppendFormat("<city>{0}</city>", city);
            if (state != null) requestContent.AppendFormat("<state>{0}</state>", state);
            if (documentNumber != null) requestContent.AppendFormat("<document-number>{0}</document-number>", documentNumber);
            if (documentAmount != null) requestContent.AppendFormat("<document-amount>{0}</document-amount>", documentAmount.Value.ToString("0.00"));
            if (discountAmount != null) requestContent.AppendFormat("<discount-amount>{0}</discount-amount>", discountAmount.Value.ToString("0.00"));
            if (percentFines != null) requestContent.AppendFormat("<percent-fines>{0}</percent-fines>", percentFines.Value.ToString("0.00"));
            if (percentInterestDay != null) requestContent.AppendFormat("<percent-interest-day>{0}</percent-interest-day>", percentInterestDay.Value.ToString("0.00"));
            if (comments != null) requestContent.AppendFormat("<comments>{0}</comments>", comments);

            requestContent.Append("</bank-billet>");

            return requestContent.ToString();
        }
    }
}
