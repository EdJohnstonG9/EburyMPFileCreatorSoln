﻿
using CsvHelper.Configuration.Attributes;

using EburyMPFileCreator.Core.Helpers;

using System;

namespace EburyMPFileCreator.Core.Models
{
    //[DelimitedRecord(","), IgnoreFirst(1),]
    public class MassPaymentFileModel
    {
        public string Direction { get; set; }
        public string Product { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryAddress1 { get; set; }
        public string BeneficiaryCity { get; set; }
        public string BeneficiaryCountry { get; set; }
        public string PaymentCurrency { get; set; }
        public decimal PaymentAmount { get; set; }
        public string SettlementCurrency { get; set; }
        public string BankName { get; set; }
        public string BankAddress1 { get; set; }
        public string BankCity { get; set; }
        public string BankCountry { get; set; }
        public string AccountNo { get; set; }
        public string IBAN { get; set; }
        public string SwiftCode { get; set; }
        public string PaymentReference { get; set; }
        public string BankCode { get; set; }
        [TypeConverter(typeof(EmpDateConverter),"ddMMyy")]
        public DateTime ValueDate { get; set; }
        public string RemitterName { get; set; }
        public string RemitterAddressLine1 { get; set; }
        public string RemitterAddressLine2 { get; set; }
        public string RemitterAccountNumber { get; set; }
        public string RemitterCountry { get; set; }
        [Name("Reason for trade")]
        //[FieldCaption("Reason for trade")]
        public string ReasonForPayment { get; set; }
        public string BeneficiaryReference { get; set; }
        public string PurposeCode { get; set; }
        public string INN { get; set; }
        public string VO { get; set; }
        public string KIO { get; set; }
        public string RussianCentralBankAccount { get; set; }
    }

    public enum DirectionCode
    {
        BUY,
        SELL
    }
}
