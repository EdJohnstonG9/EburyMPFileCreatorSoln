using ClosedXML.Excel;

using EburyMPFromFile.Models;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Helpers
{
    public static class ExcelFileHelpers
    {
        public static IXLWorkbook WriteEMPFileToExcel(this IEnumerable<MassPaymentFileModel> outputData, string path, string template)
        {
            var xlTemplate = ClosedXmlHelpers.OpenWorkbook(path, template);

            var sheet = xlTemplate?.Worksheets.First();
            if (sheet == null || sheet.Name != "MassPayments")
            {
                throw new ArgumentException($"{nameof(WriteEMPFileToExcel)}\tTemplate workbook is not a valid EMP Template: {path}/{template}");
            }
            string direction = sheet.GetCellValue<string>("A1");
            if(direction!="Direction")
            {
                throw new ArgumentException($"{nameof(WriteEMPFileToExcel)}\tTemplate workbook is not a valid EMP Template: {path}/{template}");
            }

            sheet.AddPaymentData(outputData, 1, 0);

            return xlTemplate;
        }

        internal static void AddPaymentData<T>(this IXLWorksheet worksheet, IEnumerable<T> outputData, int startRow = 0, int startCol = 0)
        {
            if (typeof(T) != typeof(MassPaymentFileModel))
            {
                throw new ApplicationException();
            }

            Type type = typeof(T);
            var properties = type.GetProperties();

            int row = startRow;
            foreach (T item in outputData)
            {
                //object[] values = new object[properties.Length];
                int col = startCol;
                foreach (var prop in properties)
                {
                    var dispAttr = prop.GetCustomAttribute(typeof(DisplayAttribute)) as DisplayAttribute;
                    if (dispAttr?.AutoGenerateField ?? true)
                    {
                        object value = prop.GetValue(item);
                        Type typ = prop.PropertyType;
                        string cellName = CellNameHepers.CellName(col, row);
                        worksheet.SetCellValue(cellName, value);
                        col += 1;
                    }
                }
                row += 1;
            }

        }

    }
}
