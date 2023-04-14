using EburyMPFromFile.Models;
using EburyMPFromFile.Models.SGGiro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Helpers.GiroHelpers
{
    internal static class GiroConverterHelpers
    {
        internal static MassPaymentFileModel GetPaymentFromGiro(this GiroItemModel giro, GiroHeaderModel header)
        {
            MassPaymentFileModel output = new MassPaymentFileModel();

            output.Direction = "BUY";
            output.Product = "SPOT";
            output.BeneficiaryName = giro.BeneficiaryName;
            output.BeneficiaryAddress1 = giro.BeneficiaryAddress1;
            output.BeneficiaryCity = giro.BeneficiaryAddress2;
            output.BeneficiaryCountry = giro.BeneficiaryAddress3;
            output.PaymentCurrency = giro.PaymentCcy;
            output.PaymentAmount = giro.PaymentAmount;
            output.SettlementCurrency = giro.PaymentCcy;//May require a manual entry?
            output.BankName = "";
            output.BankAddress1 = "";
            output.BankCity = "";
            output.BankCountry = "";
            output.AccountNo = giro.BeneficiaryAccount;
            output.IBAN = "";
            output.SwiftCode = giro.BeneficiaryBic;
            output.PaymentReference = giro.Narrative;
            output.BankCode = "";
            output.ValueDate = header.ValueDate.DateTime;
            output.RemitterName = header.ClientName;
            output.RemitterAddressLine1 = "";
            output.RemitterAddressLine2 = "";
            output.RemitterAccountNumber = "";
            output.RemitterCountry = "";
            output.ReasonForPayment = giro.ReasonCode;

            CheckBeneAddress(output);

            return output;
        }

        private static void CheckBeneAddress(MassPaymentFileModel output)
        {
            if(output.BeneficiaryAddress1.Trim().Length == 0)
            {
                output.BeneficiaryAddress1 = "-";
            }
            if(output.BeneficiaryCountry.Trim().Length == 0)
            {
                output.BeneficiaryCountry = GetCtryFromBIC(output.SwiftCode);
            }
        }

        private static string GetCtryFromBIC(string swiftCode)
        {
            if(swiftCode.Length >=6)
            {
                return swiftCode.Substring(4, 2);
            }
            else
            {
                return "";
            }
        }
    }
}
