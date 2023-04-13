using FileHelpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromSGGiro.Helpers
{
    /// <summary>
    /// Tunrs Int into string following Format i.e. 00# for zero padded 3 digits
    /// </summary>
    internal class ZeroPadUIntConverter : ConverterBase
    {
        string _format;
        public ZeroPadUIntConverter(int length)
        {
            string format = new string('0', length - 1) + "#";
            _format = format;
        }
        public override object StringToField(string from)
        {
            return uint.Parse(from);
        }
        public override string FieldToString(object from)
        {
            if (from is uint i)
            {
                string output = i.ToString(_format);
                return output;
            }
            if (from is ulong l)
            {
                string output = l.ToString(_format);
                return output;
            }
            return base.FieldToString(from);
        }
    }

    internal class FixedDateTimeOffsetConverter : ConverterBase
    {
        string _format;
        public FixedDateTimeOffsetConverter(string format)
        {
            _format = format;
        }
        public override object StringToField(string from)
        {
            DateTimeFormatInfo formatInfo = new DateTimeFormatInfo();
            formatInfo.FullDateTimePattern = _format;
            DateTimeOffset output = DateTime.ParseExact(from, _format, CultureInfo.CurrentCulture);
            return output;
        }

        public override string FieldToString(object from)
        {
            if(from is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.ToString(_format);
            }
            return base.FieldToString(from);
        }
    }
}
