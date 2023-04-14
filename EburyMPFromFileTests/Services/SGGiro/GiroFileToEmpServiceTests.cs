using Xunit;
using EburyMPFromFile.Services.SGGiro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Services.SGGiro.Tests
{
    public class GiroFileToEmpServiceTests
    {
        GiroFileToEmpService _service;
        string _testPath = @"G:\Shared drives\MP - High Wycombe - Data\GiroData";
        public GiroFileToEmpServiceTests()
        {
            _service = new GiroFileToEmpService();
        }
        [Fact()]
        public void ReadInputFileTest()
        {
            var result = _service.ReadInputFile("GiroSample.txt", _testPath);
            Assert.True(result);
            Assert.NotNull(_service.FileInput);
            Assert.True(_service.FileInput.GiroItems.Count() > 0);

            Assert.Throws<ArgumentException>(() => _service.ReadInputFile("doesnotexist.txt", _testPath));
            Assert.Contains("File Not Found", _service.ErrorText);

            Assert.Throws<ApplicationException>(() => _service.ReadInputFile("ShortFile.txt", _testPath));
            Assert.Contains("Could not process file", _service.ErrorText);

            Assert.Throws<ApplicationException>(() => _service.ReadInputFile("LongFile.txt", _testPath));
            Assert.Contains("Could not process file", _service.ErrorText);
        }

        [Fact()]
        public void ConvertToEmpDataTest()
        {
            var read = _service.ReadInputFile("GiroSample.txt", _testPath);
            Assert.True(read);

            var result = _service.ConvertToEmpData();
            Assert.NotNull(result);
            Assert.Equal(_service.FileInput.GiroItems.Count(),result.Count());
        }
    }
}