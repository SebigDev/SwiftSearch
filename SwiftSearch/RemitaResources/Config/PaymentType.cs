using System;
using System.ComponentModel.DataAnnotations;

namespace SwiftSearch.Config
{
    public enum PaymentType
    {
        [Display(Name ="ATM")]
        ATM,
        [Display(Name = "POS")]
        POS,
        VERVE,
        VISA,
        MASTERCARD,
        [Display(Name = "Union Pay")]
        UNION_PAY,
        REMITA,
        [Display(Name = "Bank Branch")]
        BANK_BRANCH,
        [Display(Name = "Bank Internet")]
        BANK_INTERNET,
        [Display(Name = "Wallet")]
        WALLET,
        [Display(Name = "RRR Gen")]
        RRRGEN
    }
}
