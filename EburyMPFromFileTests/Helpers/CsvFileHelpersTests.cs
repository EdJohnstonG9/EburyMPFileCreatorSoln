﻿using Xunit;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EburyMPFromFile.Models;
using EburyMPFromFile.Helpers;

namespace EburyMPFromFileTests.Helpers
{
    public class CsvFileHelpersTests
    {
        string _path = @"G:\Shared drives\MP - High Wycombe - Data\CSV File Examples";
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