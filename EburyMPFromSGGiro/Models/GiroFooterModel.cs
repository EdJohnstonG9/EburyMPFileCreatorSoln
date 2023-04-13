using EburyMPFromSGGiro.Helpers;

using FileHelpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromSGGiro.Models
{
    [FixedLengthRecord()]
    public class GiroFooterModel
    {
        /// <summary>
        /// Unidentified digits
        /// </summary>
        [FieldFixedLength(58)]
        public string Placeholder01 { get; set; }

        /// <summary>
        /// Unidentified Banks
        /// </summary>
        [FieldFixedLength(26)]
        public string Placeholder02 { get; set; }

        /// <summary>
        /// Unidentified digits
        /// </summary>
        [FieldFixedLength(11)]
        public string Placeholder03 { get; set; }

        /// <summary>
        /// Unidentified Blanks
        /// </summary>
        [FieldFixedLength(903)]
        public string Placeholder04 { get; set; }

        /// <summary>
        /// Probably zero filled record type field
        /// </summary>
        [FieldFixedLength(2)]
        [FieldConverter(typeof(ZeroPadUIntConverter), 2)]
        public uint RecordType { get; set; }

    }
}
