using Xunit;
using EburyMPFromFile.Helpers;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using EburyMPFromFile.Models.SGGiro;

namespace EburyMPFromFileTests.Helpers.SGGiro
{
    public class FixedReaderHelpersTests
    {
        string _exampleFile = @"15032023150507AMTEK01 290320230020001390                        INTERPLEX HOLDINGS PTE. LTD.                                                                                                                                                  00005                                   C                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               01
22Salary GIRO Transaction            DBSSSGSGXXX                        1200678749                        ALESSANDRO PERROTTA                                                                                                                         SALASGD00006805044                                                                                                                                                                               N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              10
22Salary GIRO Transaction            UOVBSGSGXXX                        5063002152                        CHEONG PUI LENG                                                                                                                             SALASGD00000514000                                                                                                                                                                               N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              10
22Salary GIRO Transaction            DBSSSGSGXXX                        118949560                         KEE PHAIK SEE                                                                                                                               SALASGD00000589800                                                                                                                                                                               N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              10
22Salary GIRO Transaction            DBSSSGSGXXX                        183138723                         SOON SWEE HAR JOCELIN                                                                                                                       SALASGD00004179700                                                                                                                                                                               N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              10
22Salary GIRO Transaction            DBSSSGSGXXX                        125178880                         TAN YUEN LIN                                                                                                                                SALASGD00000527100                                                                                                                                                                               N                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              10
0000000000500000000001261564400000000000000000000000000000                          10436345581                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       20";
        string[] _exampleFileLines;
        public FixedReaderHelpersTests()
        {
            _exampleFileLines = _exampleFile.Split(Environment.NewLine);
        }
        [Fact()]
        public void FixedRecordReadStringHeaderTest()
        {
            GiroHeaderModel result = _exampleFileLines[0].FixedRecordReadString<GiroHeaderModel>();

            Assert.NotNull(result);
            Assert.Equal((uint)1, result.RecordType);

            var output = result.FixedRecordWriteString();
            Assert.Equal(_exampleFileLines[0], output);

        }

        [Fact()]
        public void FixedRecordReadStringFooterTest()
        {
            GiroFooterModel result = _exampleFileLines[_exampleFileLines.Length - 1].FixedRecordReadString<GiroFooterModel>();

            Assert.NotNull(result);
            Assert.Equal((uint)20, result.RecordType);

            var output = result.FixedRecordWriteString();
            Assert.Equal(_exampleFileLines[_exampleFileLines.Length - 1], output);

        }


        [Fact()]
        public void FixedRecordReadStringItemTest()
        {
            for (int i = 1; i < _exampleFileLines.Length - 1; i++)
            {
                GiroItemModel result = _exampleFileLines[i].FixedRecordReadString<GiroItemModel>();

                Assert.NotNull(result);
                Assert.Equal((uint)10, result.RecordType);

                var output = result.FixedRecordWriteString();
                Debug.Print(output);
                Assert.Equal(_exampleFileLines[i], output);
            }

        }
    }
}