using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EburyMPFromFile.Helpers
{
    public static class CellNameHepers
    {
        public static string ColumnName(this string cellName)
        {
            // Create a regular expression to match the column name portion of the cell name.
            if (string.IsNullOrEmpty(cellName))
            {
                return null;
            }
            else
            {
                Regex regex = new Regex("^([A-Za-z]+)(?:[0-9]+)?$");
                Match match = regex.Match(cellName.ToUpper());

                return match.Groups[1].Value;
            }
        }

        /// <summary>
        /// Zero based Column number
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        public static int ColumnNumber(this string cellName)
        {
            string colName = cellName.ColumnName();
            int output = 0;
            int mult = 26;
            foreach (char colEl in colName)
            {
                int ind = (short)char.ToUpper(colEl) - (short)'A' + 1;
                output = ind + output * mult;
            }
            return output - 1;
        }

        /// <summary>
        /// Zero based Row Number from CellName
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        public static int RowNumber(this string cellName)
        {
            Regex regex = new Regex("^(?:[A-Za-z]+)([0-9]+)$");
            Match match = regex.Match(cellName);
            int output = -1;
            if (match.Value.Length > 0)
                output = int.Parse(match.Groups[1].Value) - 1;
            return output;
        }

        /// <summary>
        /// Zero based row/col numbers, returns CellRef
        /// </summary>
        /// <param name="col">Zero based Col</param>
        /// <param name="row">Zero based Row</param>
        /// <returns></returns>
        public static string CellName(int col, int row)
        {
            const int alphaBase = 26;
            string colName = "";
            int colNo = col + 1;
            while (colNo > 0)
            {
                int letter = (colNo - 1) % alphaBase;
                colName = Convert.ToChar(letter + 'A') + colName;
                colNo = (colNo - letter - 1) / alphaBase;
            }
            return $"{colName}{row + 1}";
        }

    }
}
