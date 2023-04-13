using Xunit;
using EburyMPFileCreator.Core.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EburyMPFileCreator.Core.Models;

namespace EburyMPFileCreator.Core.Helpers.Tests
{
    public class CsvFileHelpersTests
    {
        string _path = @"D:\VisualStudio\Source\Repos\EburyMPFileCreator\EburyMPFileCreator.Core.Tests.xUnit\TestData\";
        public CsvFileHelpersTests()
        {

        }

        [Theory()]
        [InlineData("FXFMP032857.csv", 593)]
        public void ReadMassPaymentsFileTest(string fileName, int records)
        {
            var result = CsvFileHelpers.ReadMassPaymentsFile(fileName, _path);

            Assert.NotNull(result);
            Assert.Equal(records, result.Count());
        }
    }
}