using EburyMPFromFile.Helpers;
using EburyMPFromFile.Helpers.GiroHelpers;
using EburyMPFromFile.Models;
using EburyMPFromFile.Models.SGGiro;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Services.SGGiro
{
    public class GiroFileToEmpService : FileToEmpServiceBase<GiroFileModel>
    {
#if DEBUG
        public override bool ReadInputFile(string file, string path)
        {
            return base.ReadInputFile(file, path);
        }
        public override IEnumerable<MassPaymentFileModel> ConvertToEmpData()
        {
            return base.ConvertToEmpData();
        }
#endif

        protected override IEnumerable<MassPaymentFileModel> GetEmpDataFromModel(GiroFileModel fileInput)
        {
            List<MassPaymentFileModel> output = new List<MassPaymentFileModel>();
            foreach(var item in FileInput.GiroItems)
            {
                var emp = item.GetPaymentFromGiro(FileInput.GiroHeader);
                output.Add(emp);
            }
            return output;
        }

        protected override bool GetModelFromFile(string filePath)
        {
            bool output = false;
            FileInput = new GiroFileModel();
            try
            {
                using TextReader reader = new StreamReader(filePath);
                string header = reader.ReadLine();
                FileInput.GiroHeader = header.FixedRecordReadString<GiroHeaderModel>();

                if(FileInput.GiroHeader is null || FileInput.GiroHeader.RecordType != 1)
                {
                    ErrorText = $"{nameof(GetModelFromFile)}\tFile has no or a bad Header:\n{header}";
                    throw new ApplicationException(ErrorText);
                }
                int records = (int)FileInput.GiroHeader.ItemRecords;
                var items = new List<GiroItemModel>();
                for (int i = 0; i < records; i++)
                {
                    string item = reader.ReadLine();
                    var giroItem = item.FixedRecordReadString<GiroItemModel>();
                    if (giroItem.PaymentCcy == "SGD"
                        && giroItem.PaymentAmount > 0
                        && !string.IsNullOrEmpty(giroItem.BeneficiaryBic)
                        && !string.IsNullOrEmpty(giroItem.BeneficiaryAccount))
                    {
                        items.Add(giroItem);
                    }
                    else
                    {
                        ErrorText = $"{nameof(GetModelFromFile)}\tInvalid Payment Data:\n{item}";
                        throw new ApplicationException(ErrorText);
                    }
                }
                FileInput.GiroItems = items;

                var footer = reader.ReadLine();
                FileInput.GiroFooter = footer.FixedRecordReadString<GiroFooterModel>();
                if (FileInput.GiroFooter.RecordType != 20)
                {
                    ErrorText = $"{nameof(GetModelFromFile)}\tInvalid Footer Record:\n{footer}";
                    throw new ApplicationException(ErrorText);
                }
                output = true;
                return output;
            }
            catch (Exception ex)
            {
                ErrorText = $"{nameof(GetModelFromFile)}\tCould not process file:{filePath}\n{ex.Message}";
                throw new ApplicationException(ErrorText, ex);
            }
        }
    }
}
