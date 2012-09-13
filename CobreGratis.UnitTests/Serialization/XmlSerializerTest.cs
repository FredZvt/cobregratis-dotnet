using System;
using System.Xml;
using BielSystems;
using BielSystems.Log;
using BielSystems.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Serialization
{
    [TestClass]
    public class XmlSerializerTest
    {
        [TestMethod]
        public void DeserializeXmlNodeToObject_should_work()
        {
            #region Looooong xml string

            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <bank-billets type=""array"">
                  <bank-billet>
                    <acceptance>N</acceptance>
                    <account-id type=""integer"">15424</account-id>
                    <address>Texto Endere&#231;o</address>
                    <amount type=""float"">2.0</amount>
                    <assignor-code>1331/9003433/6</assignor-code>
                    <bank-billet-account-id type=""integer"">2933</bank-billet-account-id>
                    <city>Texto Cidade</city>
                    <cnpj-cpf>944.415.409-22</cnpj-cpf>
                    <comments nil=""true""></comments>
                    <created-at type=""datetime"">2012-09-02T19:41:33-03:00</created-at>
                    <created-by-api type=""boolean"">false</created-by-api>
                    <currency>R$</currency>
                    <customer-id type=""integer"" nil=""true""></customer-id>
                    <decimal-character>,</decimal-character>
                    <deleted-at type=""datetime"" nil=""true""></deleted-at>
                    <description>Texto Demostrativo</description>
                    <discount-amount type=""decimal"">10.0</discount-amount>
                    <document-amount type=""decimal"">2.0</document-amount>
                    <document-date type=""date"">2012-09-08</document-date>
                    <document-number>Texto N&#250;mero do Doc</document-number>
                    <document-type>Outros</document-type>
                    <due-date-business-day nil=""true""></due-date-business-day>
                    <email-delayed-at type=""datetime"" nil=""true""></email-delayed-at>
                    <email-sent-at type=""datetime"" nil=""true""></email-sent-at>
                    <email-state type=""integer"" nil=""true""></email-state>
                    <expire-at type=""date"">2016-09-17</expire-at>
                    <generated-at type=""datetime"" nil=""true""></generated-at>
                    <guarantor>23</guarantor>
                    <homologation type=""boolean"">false</homologation>
                    <html-created-at type=""datetime"">2012-09-02T19:41:35-03:00</html-created-at>
                    <html-delayed-at type=""datetime"">2012-09-02T19:41:33-03:00</html-delayed-at>
                    <html-state type=""integer"">1</html-state>
                    <id type=""integer"">63714</id>
                    <instructions>Texto Instru&#231;&#245;es</instructions>
                    <kind>normal</kind>
                    <line>35691.33196 00343.360996 99999.990023 2 69200000000200</line>
                    <meta type=""binary"" nil=""true"" encoding=""base64""></meta>
                    <name>Sacado da Silva</name>
                    <neighborhood>Texto Bairro</neighborhood>
                    <notify-overdue type=""boolean"">false</notify-overdue>
                    <other-amount-to-add type=""decimal"">20.0</other-amount-to-add>
                    <our-number>0999999999002</our-number>
                    <our-number-prefix nil=""true""></our-number-prefix>
                    <overdue-notified-at type=""datetime"" nil=""true""></overdue-notified-at>
                    <paid-amount type=""float"" nil=""true""></paid-amount>
                    <paid-at type=""date"" nil=""true""></paid-at>
                    <parcel type=""integer"">1</parcel>
                    <pdf-created-at type=""datetime"">2012-09-02T19:41:33-03:00</pdf-created-at>
                    <pdf-delayed-at type=""datetime"">2012-09-02T19:41:33-03:00</pdf-delayed-at>
                    <pdf-state type=""integer"">1</pdf-state>
                    <percent-fines type=""decimal"">15.0</percent-fines>
                    <percent-interest-day type=""decimal"">5.41</percent-interest-day>
                    <processed-our-number>0999999999002</processed-our-number>
                    <quantity nil=""true""></quantity>
                    <service-id type=""integer"">15499</service-id>
                    <state>RJ</state>
                    <status>o</status>
                    <subdivision-parent-id type=""integer"" nil=""true""></subdivision-parent-id>
                    <thousand-character>.</thousand-character>
                    <updated-at type=""datetime"">2012-09-02T19:41:35-03:00</updated-at>
                    <zipcode nil=""true""></zipcode>
                    <external-link>http://bole.to/8q4h1d5u</external-link>
                    <hashcode>8q4h1d5u</hashcode>
                  </bank-billet>
                  <bank-billet>
                    <acceptance>N</acceptance>
                    <account-id type=""integer"">15424</account-id>
                    <address>Rua das ruas, 51</address>
                    <amount type=""float"">1.0</amount>
                    <assignor-code>1331/9003433/7</assignor-code>
                    <bank-billet-account-id type=""integer"">2933</bank-billet-account-id>
                    <city>Niter&#243;i</city>
                    <cnpj-cpf>221.761.177-19</cnpj-cpf>
                    <comments nil=""true""></comments>
                    <created-at type=""datetime"">2012-09-01T20:12:45-03:00</created-at>
                    <created-by-api type=""boolean"">false</created-by-api>
                    <currency>R$</currency>
                    <customer-id type=""integer"" nil=""true""></customer-id>
                    <decimal-character>,</decimal-character>
                    <deleted-at type=""datetime"" nil=""true""></deleted-at>
                    <description>Texto Demonstrativo</description>
                    <discount-amount type=""decimal"" nil=""true""></discount-amount>
                    <document-amount type=""decimal"" nil=""true""></document-amount>
                    <document-date type=""date"" nil=""true""></document-date>
                    <document-number></document-number>
                    <document-type></document-type>
                    <due-date-business-day nil=""true""></due-date-business-day>
                    <email-delayed-at type=""datetime"" nil=""true""></email-delayed-at>
                    <email-sent-at type=""datetime"" nil=""true""></email-sent-at>
                    <email-state type=""integer"" nil=""true""></email-state>
                    <expire-at type=""date"">2012-09-08</expire-at>
                    <generated-at type=""datetime"" nil=""true""></generated-at>
                    <guarantor></guarantor>
                    <homologation type=""boolean"">false</homologation>
                    <html-created-at type=""datetime"">2012-09-01T20:12:47-03:00</html-created-at>
                    <html-delayed-at type=""datetime"">2012-09-01T20:12:45-03:00</html-delayed-at>
                    <html-state type=""integer"">1</html-state>
                    <id type=""integer"">63620</id>
                    <instructions>Texto Instru&#231;&#245;es</instructions>
                    <kind>normal</kind>
                    <line>35691.33196 00343.370995 99999.990015 5 54500000000100</line>
                    <meta type=""binary"" nil=""true"" encoding=""base64""></meta>
                    <name>Fulano da Silva</name>
                    <neighborhood>S&#227;o Francisco</neighborhood>
                    <notify-overdue type=""boolean"">false</notify-overdue>
                    <other-amount-to-add type=""decimal"" nil=""true""></other-amount-to-add>
                    <our-number>0999999999001</our-number>
                    <our-number-prefix nil=""true""></our-number-prefix>
                    <overdue-notified-at type=""datetime"" nil=""true""></overdue-notified-at>
                    <paid-amount type=""float"" nil=""true""></paid-amount>
                    <paid-at type=""date"" nil=""true""></paid-at>
                    <parcel type=""integer"">1</parcel>
                    <pdf-created-at type=""datetime"">2012-09-01T20:12:45-03:00</pdf-created-at>
                    <pdf-delayed-at type=""datetime"">2012-09-01T20:12:45-03:00</pdf-delayed-at>
                    <pdf-state type=""integer"">1</pdf-state>
                    <percent-fines type=""decimal"" nil=""true""></percent-fines>
                    <percent-interest-day type=""decimal"" nil=""true""></percent-interest-day>
                    <processed-our-number>0999999999001</processed-our-number>
                    <quantity nil=""true""></quantity>
                    <service-id type=""integer"">15499</service-id>
                    <state>RJ</state>
                    <status>o</status>
                    <subdivision-parent-id type=""integer"" nil=""true""></subdivision-parent-id>
                    <thousand-character>.</thousand-character>
                    <updated-at type=""datetime"">2012-09-01T20:12:47-03:00</updated-at>
                    <zipcode>24360-018</zipcode>
                    <external-link>http://bole.to/8q611d38</external-link>
                    <hashcode>8q611d38</hashcode>
                  </bank-billet>
                </bank-billets>
			";

            #endregion

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var nodes = xmlDoc.SelectNodes("//bank-billet");
            Assert.AreEqual(2, nodes.Count);

            var serializer = new XmlSerializer(new LogHelper(null));
            var billet = serializer.DeserializeXmlNodeToObject<BankBillet>(nodes[0]);

            Assert.AreEqual("N", billet.Acceptance);
            Assert.AreEqual(15424, billet.AccountId);
            Assert.AreEqual("Texto Endereço", billet.Address);
            Assert.AreEqual(true, billet.Amount.HasValue);
            Assert.AreEqual(2.0M, billet.Amount.Value);
            Assert.AreEqual("1331/9003433/6", billet.AssignorCode);
            Assert.AreEqual(true, billet.BankBilletAccountId.HasValue);
            Assert.AreEqual(2933, billet.BankBilletAccountId);
            Assert.AreEqual("Texto Cidade", billet.City);
            Assert.AreEqual("944.415.409-22", billet.CnpjCpf);
            Assert.AreEqual(null, billet.Comments);
            Assert.AreEqual(true, billet.CreatedAt.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 33).Ticks, billet.CreatedAt.Value.Ticks);
            Assert.AreEqual(true, billet.CreatedByApi.HasValue);
            Assert.AreEqual(false, billet.CreatedByApi.Value);
            Assert.AreEqual("R$", billet.Currency);
            Assert.AreEqual(false, billet.CustomerId.HasValue);
            Assert.AreEqual(",", billet.DecimalCharacter);
            Assert.AreEqual(false, billet.DeletedAt.HasValue);
            Assert.AreEqual("Texto Demostrativo", billet.Description);
            Assert.AreEqual(true, billet.DiscountAmount.HasValue);
            Assert.AreEqual(10.0M, billet.DiscountAmount.Value);
            Assert.AreEqual(true, billet.DocumentAmount.HasValue);
            Assert.AreEqual(2.0M, billet.DocumentAmount.Value);
            Assert.AreEqual(true, billet.DocumentDate.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 8).Ticks, billet.DocumentDate.Value.Ticks);
            Assert.AreEqual("Texto Número do Doc", billet.DocumentNumber);
            Assert.AreEqual("Outros", billet.DocumentType);
            Assert.AreEqual(null, billet.DueDateBusinessDay);
            Assert.AreEqual(false, billet.EmailDelayedAt.HasValue);
            Assert.AreEqual(false, billet.EmailSentAt.HasValue);
            Assert.AreEqual(false, billet.EmailState.HasValue);
            Assert.AreEqual(true, billet.ExpireAt.HasValue);
            Assert.AreEqual(new DateTime(2016, 9, 17).Ticks, billet.ExpireAt.Value.Ticks);
            Assert.AreEqual(false, billet.GeneratedAt.HasValue);
            Assert.AreEqual("23", billet.Guarantor);
            Assert.AreEqual(false, billet.Homologation);
            Assert.AreEqual(true, billet.HtmlCreatedAt.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 35).Ticks, billet.HtmlCreatedAt.Value.Ticks);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 33).Ticks, billet.HtmlDelayedAt.Value.Ticks);
            Assert.AreEqual(true, billet.HtmlState.HasValue);
            Assert.AreEqual(1, billet.HtmlState.Value);
            Assert.AreEqual(true, billet.Id.HasValue);
            Assert.AreEqual(63714, billet.Id.Value);
            Assert.AreEqual("Texto Instruções", billet.Instructions);
            Assert.AreEqual("normal", billet.Kind);
            Assert.AreEqual("35691.33196 00343.360996 99999.990023 2 69200000000200", billet.Line);
            Assert.AreEqual(null, billet.Meta);
            Assert.AreEqual("Sacado da Silva", billet.Name);
            Assert.AreEqual("Texto Bairro", billet.Neighborhood);
            Assert.AreEqual(true, billet.NotifyOverdue.HasValue);
            Assert.AreEqual(false, billet.NotifyOverdue.Value);
            Assert.AreEqual(true, billet.OtherAmountToAdd.HasValue);
            Assert.AreEqual(20.0M, billet.OtherAmountToAdd.Value);
            Assert.AreEqual("0999999999002", billet.OurNumber);
            Assert.AreEqual(null, billet.OurNumberPrefix);
            Assert.AreEqual(false, billet.OverdueNotifiedAt.HasValue);
            Assert.AreEqual(false, billet.PaidAmount.HasValue);
            Assert.AreEqual(false, billet.PaidAt.HasValue);
            Assert.AreEqual(true, billet.Parcel.HasValue);
            Assert.AreEqual(1, billet.Parcel.Value);
            Assert.AreEqual(true, billet.PdfCreatedAt.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 33).Ticks, billet.PdfCreatedAt.Value.Ticks);
            Assert.AreEqual(true, billet.PdfDelayedAt.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 33).Ticks, billet.PdfDelayedAt.Value.Ticks);
            Assert.AreEqual(true, billet.PdfState.HasValue);
            Assert.AreEqual(1, billet.PdfState.Value);
            Assert.AreEqual(true, billet.PercentFines.HasValue);
            Assert.AreEqual(15M, billet.PercentFines.Value);
            Assert.AreEqual(true, billet.PercentInterestDay.HasValue);
            Assert.AreEqual(5.41M, billet.PercentInterestDay.Value);
            Assert.AreEqual("0999999999002", billet.ProcessedOurNumber);
            Assert.AreEqual(null, billet.Quantity);
            Assert.AreEqual(true, billet.ServiceId.HasValue);
            Assert.AreEqual(15499, billet.ServiceId.Value);
            Assert.AreEqual("RJ", billet.State);
            Assert.AreEqual("o", billet.Status);
            Assert.AreEqual(false, billet.SubdivisionParentId.HasValue);
            Assert.AreEqual(".", billet.ThousandCharacter);
            Assert.AreEqual(true, billet.UpdatedAt.HasValue);
            Assert.AreEqual(new DateTime(2012, 9, 2, 19, 41, 35).Ticks, billet.UpdatedAt.Value.Ticks);
            Assert.AreEqual(null, billet.Zipcode);
            Assert.AreEqual("http://bole.to/8q4h1d5u", billet.ExternalLink);
            Assert.AreEqual("8q4h1d5u", billet.Hashcode);
        }
    }
}
