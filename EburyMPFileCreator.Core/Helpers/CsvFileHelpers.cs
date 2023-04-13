using CsvHelper;
using CsvHelper.Configuration;

using EburyMPFileCreator.Core.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EburyMPFileCreator.Core.Helpers
{
    public static class CsvFileHelpers
    {
        public static IEnumerable<MassPaymentFileModel> ReadMassPaymentsFile(string fileName, string filePath)
        {
            IEnumerable<MassPaymentFileModel> output;
            var fi = new FileInfo(Path.Combine(filePath, fileName));
            if (!fi.Exists)
            {
                throw new ArgumentException($"{nameof(ReadMassPaymentsFile)}\tRequested file does not exist: {fi.FullName}");
            }
            try
            {
                using (TextReader reader = new StreamReader(fi.FullName))
                using (var csv = new CsvReader(reader, configuration: GetCsvConfig()))
                {
                    output = csv.GetRecords<MassPaymentFileModel>().ToList();
                    return output;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static bool WriteMassPaymentsFile(this IEnumerable<MassPaymentFileModel> data, string FileName)
        {
            bool success = false;
            try
            {
                var fi = new FileInfo(FileName);
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                if (fi.Exists) fi.Delete();
                CsvConfiguration csvConfig = GetCsvConfig();

                using (var writer = new StreamWriter(FileName))
                using (var csv = new CsvWriter(writer, csvConfig))
                {
                    csv.WriteRecords(data);
                }
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }

        private static CsvConfiguration GetCsvConfig()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture);
            csvConfig.PrepareHeaderForMatch = (header) => Regex.Replace(header.Header, @"\s", replacement: string.Empty);
            csvConfig.MissingFieldFound = null;
            csvConfig.HeaderValidated = null;
            csvConfig.IgnoreBlankLines = true;
            csvConfig.ShouldSkipRecord = (records) => records.Row [0].ToString() == "" && records.Row[1].ToString() == "";
            //csvConfig.TypeConverterOptionsCache.GetOptions<DateTime>().Formats = new[] { "dd/MM/yyyy" };
            return csvConfig;
        }
    }
}
