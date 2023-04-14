using ClosedXML.Excel;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EburyMPFromFile.Helpers
{
    public static class ClosedXmlHelpers
    {
        public static IXLWorkbook CreateWorkbook(string sheetName = null)
        {
            var output = new XLWorkbook();
            if (sheetName != null)
                output.AddGetWorksheet(sheetName);
            return output;
        }

        public static IXLWorkbook OpenWorkbook(string path, string file)
        {
            string fullPath = Path.Combine(path ?? "", file);
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Exists)
                throw new ArgumentException($"{nameof(OpenWorkbook)}\tCannot find XL file: {fullPath}");
            try
            {
                var output = new XLWorkbook(fullPath);
                return output;
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{nameof(OpenWorkbook)}\tCannot open XL file: {fullPath}\n{ex.Message}", ex);
            }
        }

        public static void SaveWorkbook(this IXLWorkbook workbook, string path = default, string file = default, bool overwrite = false)
        {
            string fullPath = Path.Combine(path ?? "", file ?? "");
            try
            {
                if (string.IsNullOrEmpty(fullPath))
                {
                    workbook.Save();
                }
                else
                {
                    DirectoryInfo di = new DirectoryInfo(fullPath).Parent;
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                    FileInfo fi = new FileInfo(fullPath);
                    if (fi.Exists)
                    {
                        if (overwrite)
                            fi.Delete();
                        else
                        {
                            string backName = $"{fi.Name}-{DateTime.Now.ToString("MMddmmss")}.bak";
                            string backPath = $"{new DirectoryInfo(fullPath).Parent}\\bak";
                            DirectoryInfo diBak = new DirectoryInfo(backPath);
                            if (!diBak.Exists)
                            {
                                diBak.Create();
                            }
                            fi.MoveTo(Path.Combine(backPath, backName));
                        }
                    }
                    workbook.SaveAs(fullPath);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"{nameof(SaveWorkbook)}\tUnable to save the workbook to output: {fullPath}\n{ex.Message}", ex);
            }
        }

        public static IXLWorksheet GetWorksheet(this IXLWorkbook workbook, string sheetName)
        {
            if (workbook.TryGetWorksheet(sheetName.SheetNameValid(), out IXLWorksheet output))
            {
                return output;
            }
            return null;
        }

        public static IXLWorksheet AddGetWorksheet(this IXLWorkbook workbook, string sheetName = null)
        {
            if (workbook == null)
                throw new ArgumentException($"{nameof(AddGetWorksheet)}\tWorkboon cannot be null");

            if (sheetName == null)
            {
                var sheets = workbook.Worksheets.Select(x => x.Name);
                int i = 0;
                do
                {
                    i += 1;
                    sheetName = $"Sheet{i}";
                } while (sheets.Contains(sheetName));
            }
            sheetName = sheetName.SheetNameValid();
            var output = workbook.GetWorksheet(sheetName);
            if (output == null && !string.IsNullOrEmpty(sheetName))
                output = workbook.AddWorksheet(sheetName);

            return output;
        }

        private static string SheetNameValid(this string sheetName)
        {
            return Regex.Replace(sheetName, @"[:\/?*[\]]", "");
        }
        public static bool DelWorksheet(this IXLWorkbook workbook, string sheetName)
        {
            bool output = false;
            sheetName = sheetName.SheetNameValid();
            var sheet = workbook.GetWorksheet(sheetName);
            if (sheet != null)
            {
                try
                {
                    workbook.Worksheet(sheetName).Delete();
                    output = workbook.GetWorksheet(sheetName) == null;
                }
                catch (Exception ex)
                {
                    throw new ArgumentException($"{nameof(DelWorksheet)}\tCannot Delete XL Sheet: {sheetName}", ex);
                }
            }

            return output;
        }

        public static T GetCellValue<T>(this IXLWorksheet worksheet, string cellName)
        {
            T output = (T)Convert.ChangeType(worksheet.Cell(cellName).Value.GetText(), typeof(T));

            return output;
        }
        public static T GetCellValue<T>(this IXLWorksheet worksheet, int iCol, int iRow)
        {
            T output;
            string cellName = CellNameHepers.CellName(iCol, iRow);
            if (string.IsNullOrEmpty(worksheet.Cell(cellName).GetString()))
                output = default;
            else
                output = (T)Convert.ChangeType(worksheet.Cell(cellName).Value, typeof(T));

            return output;
        }

        public static string GetCellName(this IXLCell cell)
        {
            string output = CellNameHepers.CellName(cell.Address.ColumnNumber, cell.Address.RowNumber);

            return output;
        }


        public static void SetCellValue<T>(this IXLWorksheet worksheet, string cellName, T value)
        {
            var cell = worksheet.Cell(cellName);
            cell?.SetCellValue(value);
        }

        //public static bool SetCellValueAttrib(this IXLCell cell, dynamic value, string typeName)
        //{
        //    bool output = true;
        //    if (value != null)
        //    {
        //        switch (typeName)
        //        {
        //            case "Money":
        //                cell.Value= (decimal)value;
        //                cell.Style.NumberFormat.NumberFormatId = 4;
        //                break;
        //            case "Percent":
        //                //cell.SetDataType(XLDataType.Number);
        //                cell.SetValue((double)value);
        //                cell.Style.NumberFormat.NumberFormatId = 10;
        //                break;
        //            case "Int":
        //                //cell.SetDataType(XLDataType.Number);
        //                cell.SetValue((int)value);
        //                cell.Style.NumberFormat.NumberFormatId = 1;
        //                break;
        //            case "String":
        //                //cell.SetDataType(XLDataType.Text);
        //                cell.SetValue((string)value);
        //                //cell.Value = "'" + s;
        //                break;
        //            case "ExRate":
        //                //cell.SetDataType(XLDataType.Number);
        //                cell.SetValue((double)value);
        //                cell.Style.NumberFormat.NumberFormatId = 0;
        //                break;
        //            case default(string):
        //                output = false;
        //                break;
        //            default:
        //                output = false;
        //                break;
        //        }
        //    }
        //    else
        //    {
        //        output = false;
        //    }
        //    return output;
        //}

        public static bool SetCellValue<T>(this IXLCell cell, T value, string typeName = default)
        {
            bool output = true;
            //Type type = typeof(T);
            //if (!string.IsNullOrEmpty(typeName))
            //    type = Type.GetType(typeName);
            //if (value != null)
            {
                switch (value)
                {
                    case DateTime dt:
                        //cell.SetDataType(XLDataType.DateTime);
                        cell.SetValue(dt);
                        break;
                    case string s:
                        //cell.SetDataType(XLDataType.Text);
                        cell.SetValue(s);
                        cell.Style.NumberFormat.NumberFormatId = 49;
                        //cell.Value = "'" + s;
                        break;
                    case int i:
                        //cell.SetDataType(XLDataType.Number);
                        cell.SetValue(i);
                        cell.Style.NumberFormat.NumberFormatId = 1;
                        break;
                    case double d:
                        //cell.SetDataType(XLDataType.Number);
                        cell.SetValue(d);
                        cell.Style.NumberFormat.NumberFormatId = 0;
                        break;
                    case float f:
                        //cell.SetDataType(XLDataType.Number);
                        cell.SetValue(f);
                        cell.Style.NumberFormat.NumberFormatId = 4;
                        break;
                    case decimal di:
                        //cell.SetDataType(XLDataType.Number);
                        cell.SetValue(di);
                        cell.Style.NumberFormat.NumberFormatId = 4;
                        break;
                    case null:
                        cell.SetValue(string.Empty);
                        //output = false;
                        break;
                    default:
                        output = false;
                        break;
                }
            }
            //else
            //{
            //    output = false;
            //}
            return output;
        }

    }
}
