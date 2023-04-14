using FileHelpers;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Helpers
{
    /// <summary>
    /// Converts money amount as Decimal / text number of cents
    /// </summary>
    internal class PennyConverter : ConverterBase
    {
        string _format;
        public PennyConverter(int length = 10)
        {
            _format = new string('0', length - 1) + "#";
        }
        public override object StringToField(string from)
        {
            if (decimal.TryParse(from, out decimal output))
            {
                output = output / 100;
                return output;
            }
            else
            {
                throw new ArgumentException($"{typeof(PennyConverter)}\tCould not convert to money:{from}");
            }
        }
        public override string FieldToString(object from)
        {
            if (from is decimal decFrom)
            {
                if (ulong.TryParse((decFrom * 100m).ToString("0#"), out ulong pennies))
                {
                    var output =  pennies.ToString(_format);
                    return output;
                }
            }
            throw new ArgumentException($"{typeof(PennyConverter)}\tCould not convert to money:{from}");
        }
    }

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
