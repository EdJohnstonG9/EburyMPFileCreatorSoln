using EburyMPFromFile.Helpers;

using FileHelpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Models.SGGiro
{
    [FixedLengthRecord()]
    public class GiroHeaderModel
    {
        /// <summary>
        /// File creation date
        /// </summary>
        [FieldFixedLength(14)]
        [DefaultValue("() => DateTime.Now")]
        [FieldConverter(typeof(FixedDateTimeOffsetConverter), "ddMMyyyyHHmmss")]
        public DateTimeOffset CreatedDate { get; set; }

        [FieldFixedLength(8)]
        public string ClientCode { get; set; }

        /// <summary>
        /// File creation date
        /// </summary>
        [FieldFixedLength(8)]
        [DefaultValue("() => DateTime.Today")]
        [FieldConverter(typeof(FixedDateTimeOffsetConverter), "ddMMyyyy")]
        public DateTimeOffset ValueDate { get; set; }

        /// <summary>
        /// Unidentified string of digits
        /// </summary>
        [FieldFixedLength(10)]
        public string Placeholder01 { get; set; }

        /// <summary>
        /// string contains client name
        /// Unsure if field is this length
        /// </summary>
        [FieldFixedLength(52)]
        public string ClientName { get; set; }

        /// <summary>
        /// Unidentified blank string
        /// </summary>
        [FieldFixedLength(146)]
        public string Placeholder02 { get; set; }

        /// <summary>
        /// Probably zero filled count of items in file
        /// </summary>
        [FieldFixedLength(5)]
        [FieldConverter(typeof(ZeroPadUIntConverter), 5)]
        public uint ItemRecords { get; set; }

        /// <summary>
        /// Unidentified blank string
        /// </summary>
        [FieldFixedLength(35)]
        public string Placeholder03 { get; set; }

        /// <summary>
        /// Unidentified single character
        /// </summary>
        [FieldFixedLength(1)]
        [FieldNullValue(' ')]
        public char Placeholder04 { get; set; }

        /// <summary>
        /// Unidentified blank string
        /// </summary>
        [FieldFixedLength(719)]
        public string Placeholder05 { get; set; }

        /// <summary>
        /// Probably zero filled record type field
        /// </summary>
        [FieldFixedLength(2)]
        [FieldConverter(typeof(ZeroPadUIntConverter), 2)]
        public uint RecordType { get; set; }
    }
}
