using EburyMPFromFile.Helpers;

using FileHelpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Models.SGGiro
{
    [FixedLengthRecord]
    public class GiroItemModel
    {
        /// <summary>
        /// Unidentified digits
        /// </summary>
        [FieldFixedLength(2)]
        public string Placeholder01 { get; set; }


        /// <summary>
        /// Narrative
        /// </summary>
        [FieldFixedLength(35)]
        public string Narrative { get; set; }


        /// <summary>
        /// Bene Bank BIC
        /// </summary>
        [FieldFixedLength(11)]
        public string BeneficiaryBic { get; set; }

        /// <summary>
        /// Unidentified blanks
        /// </summary>
        [FieldFixedLength(24)]
        public string Placeholder02 { get; set; }


        /// <summary>
        /// Bene Bank Account
        /// </summary>
        [FieldFixedLength(16)]
        public string BeneficiaryAccount { get; set; }

        /// <summary>
        /// Unidentified blanks
        /// </summary>
        [FieldFixedLength(18)]
        public string Placeholder03 { get; set; }

        /// <summary>
        /// Probably space for Name followed by Addres in 140 chars
        /// </summary>
        [FieldFixedLength(35)]
        public string BeneficiaryName { get; set; }

        /// <summary>
        /// Probably space for Name followed by Addres in 140 chars
        /// </summary>
        [FieldFixedLength(35)]
        public string BeneficiaryAddress1 { get; set; }

        /// <summary>
        /// Probably space for Name followed by Addres in 140 chars
        /// </summary>
        [FieldFixedLength(35)]
        public string BeneficiaryAddress2 { get; set; }

        /// <summary>
        /// Probably space for Name followed by Addres in 140 chars
        /// </summary>
        [FieldFixedLength(35)]
        public string BeneficiaryAddress3 { get; set; }

        /// <summary>
        /// Reason for Payment
        /// </summary>
        [FieldFixedLength(4)]
        public string ReasonCode { get; set; }

        /// <summary>
        /// Ccy 3
        /// </summary>
        [FieldFixedLength(3)]
        public string PaymentCcy { get; set; }

        /// <summary>
        /// Reason for Payment
        /// </summary>
        [FieldFixedLength(11)]
        [FieldConverter(typeof(PennyConverter), 11)]//, Parameters = @"'v => Decimal.Parse(v)/100.00', 'v => v.ToString(""#.00"").Replace(""."", """")'")]
        public decimal PaymentAmount { get; set; }

        /// <summary>
        /// Unidentified blanks
        /// </summary>
        [FieldFixedLength(175)]
        public string Placeholder04 { get; set; }


        /// <summary>
        /// Unidentified single char
        /// </summary>
        [FieldFixedLength(1)]
        public string Placeholder05 { get; set; }

        /// <summary>
        /// Unidentified single char
        /// </summary>
        [FieldFixedLength(558)]
        public string Placeholder06 { get; set; }

        /// <summary>
        /// Probably zero filled record type field
        /// </summary>
        [FieldFixedLength(2)]
        [FieldConverter(typeof(ZeroPadUIntConverter), 2)]
        public uint RecordType { get; set; }
    }
}
