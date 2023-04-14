using Xunit;
using EburyMPFromFile.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Helpers.Tests
{
    public class ExcelFileHelpersTests
    {
        string _path = @"G:\Shared drives\MP - High Wycombe - Data\CSV File Examples";

        [Theory()]
        [InlineData("FXFMP032857 zeros.csv", 593)]
        public void WriteEMPFileToExcelTest(string fileName, int records)
        {
            var outputData = CsvFileHelpers.ReadMassPaymentsFile(fileName, _path);
            Assert.NotNull(outputData);
            Assert.Equal(records, outputData.Count());

            string templatePath = Path.Combine(Environment.CurrentDirectory, "Templates");
            string template = "EMPFileTemplate.xlsx";
            var result = ExcelFileHelpers.WriteEMPFileToExcel(outputData, templatePath, template);

            result.SaveWorkbook(templatePath, "output.xlsx", true);
        }
    }
}