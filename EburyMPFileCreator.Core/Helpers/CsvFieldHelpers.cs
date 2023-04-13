using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFileCreator.Core.Helpers
{
    internal class EmpDateConverter : ITypeConverter
    {
        private string _dateFormat;

        public EmpDateConverter(string dateFormat)
        {
            _dateFormat = dateFormat;
        }
        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            return DateTime.Parse(text);
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            if (value is DateTime dtVal)// || value is DateTimeOffset)
            {
                return dtVal.ToString(_dateFormat);
            }
            else if (value is DateTimeOffset dtoVal)
            {
                return dtoVal.ToString(_dateFormat);
            }
            return string.Empty;
        }
    }
}
