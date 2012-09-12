using System;

namespace BielSystems
{
    public class BankBillet
    {
        public string Acceptance { get; set; }
        public int? AccountId { get; set; }
        public string Address { get; set; }
        public decimal? Amount { get; set; }
        public string AssignorCode { get; set; }
        public int? BankBilletAccountId { get; set; }
        public string City { get; set; }
        public string CnpjCpf { get; set; }
        public string Comments { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? CreatedByApi { get; set; }
        public string Currency { get; set; }
        public int? CustomerId { get; set; }
        public string DecimalCharacter { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Description { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DocumentAmount { get; set; }
        public DateTime? DocumentDate { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentType { get; set; }
        public string DueDateBusinessDay { get; set; }
        public DateTime? EmailDelayedAt { get; set; }
        public DateTime? EmailSentAt { get; set; }
        public int? EmailState { get; set; }
        public DateTime? ExpireAt { get; set; }
        public DateTime? GeneratedAt { get; set; }
        public string Guarantor { get; set; }
        public bool? Homologation { get; set; }
        public DateTime? HtmlCreatedAt { get; set; }
        public DateTime? HtmlDelayedAt { get; set; }
        public int? HtmlState { get; set; }
        public int? Id { get; set; }
        public string Instructions { get; set; }
        public string Kind { get; set; }
        public string Line { get; set; }
        public byte[] Meta { get; set; }
        public string Name { get; set; }
        public string Neighborhood { get; set; }
        public bool? NotifyOverdue { get; set; }
        public decimal? OtherAmountToAdd { get; set; }
        public string OurNumber { get; set; }
        public string OurNumberPrefix { get; set; }
        public DateTime? OverdueNotifiedAt { get; set; }
        public float? PaidAmount { get; set; }
        public DateTime? PaidAt { get; set; }
        public int? Parcel { get; set; }
        public DateTime? PdfCreatedAt { get; set; }
        public DateTime? PdfDelayedAt { get; set; }
        public int? PdfState { get; set; }
        public decimal? PercentFines { get; set; }
        public decimal? PercentInterestDay { get; set; }
        public string ProcessedOurNumber { get; set; }
        public string Quantity { get; set; }
        public int? ServiceId { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public int? SubdivisionParentId { get; set; }
        public string ThousandCharacter { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string Zipcode { get; set; }
        public string ExternalLink { get; set; }
        public string Hashcode { get; set; }
    }
}
