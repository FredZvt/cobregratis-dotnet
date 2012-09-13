using System;
using System.Collections.Generic;

namespace BielSystems
{
    public interface ICobreGratis
    {
        string EndPoint { get; set; }
        string AuthToken { get; set; }
        string ClientAppIdentification { get; set; }
        bool EnableCache { get; set; }

        IEnumerable<BankBillet> GetBankBillets(int? page = null);
        
        BankBillet GetBankBillet(int billetId);
        
        void DeleteBillet(int billetId);

        BankBillet CreateBankBillet(
            int accountId, decimal amount, DateTime expireAt, string name, string description = null,
            string instructions = null, string cnpjCpf = null, string address = null, string zipcode = null,
            string neighborhood = null, string city = null, string state = null,
            string documentNumber = null, decimal? documentAmount = null, decimal? discountAmount = null,
            decimal? percentFines = null, decimal? percentInterestDay = null, string comments = null
        );

        void UpdateBankBillet(
            int billetId, 
            int? accountId = null, decimal? amount = null, DateTime? expireAt = null, string name = null, string description = null, 
            string instructions = null, string cnpjCpf = null, string address = null, string zipcode = null, 
            string neighborhood = null, string city = null, string state = null, string documentNumber = null, 
            decimal? documentAmount = null, decimal? discountAmount = null, decimal? percentFines = null, 
            decimal? percentInterestDay = null, string comments = null
        );
    }
}
