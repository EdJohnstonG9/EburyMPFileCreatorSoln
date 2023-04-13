
using FileHelpers;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EburyMPFromSGGiro.Helpers
{
    public static class FixedReaderHelpers
    {
        /// <summary>
        /// Takes a single Nacha record type and writes it as an output string
        /// </summary>
        /// <typeparam name="T">record data type</typeparam>
        /// <param name="input">record data</param>
        /// <returns>data file line</returns>
        public static string FixedRecordWriteString<T>(this T input) where T : class
        {
            var engine = new FixedFileEngine<T>();
            var output = engine.WriteString(new List<T>() { input });
            return output.TrimEnd(Environment.NewLine.ToCharArray());
        }

        public static void FixedRecordWriteStream<T>(this T input, StreamWriter writer) where T : class
        {
            var engine = new FixedFileEngine<T>();
            engine.WriteStream(writer, new List<T>() { input });
        }

        /// <summary>
        /// Takes a single record of the expected type and returns a single record class
        /// </summary>
        /// <typeparam name="T">model type</typeparam>
        /// <param name="input">Single line of fixed file</param>
        /// <returns>Typed object</returns>
        public static T FixedRecordReadString<T>(this string input) where T : class
        {
            FixedFileEngine<T> engine = new FixedFileEngine<T>();
            IEnumerable<T> output = engine.ReadString(input);
            if (output == null || output.Count() != 1)
            {
                throw new ArgumentOutOfRangeException($"{nameof(FixedRecordReadString)}\tExpecting exactly 1 record to be returned\n{input}\t{output.GetType().ToString()}");
            }
            return output.First();
        }
    }

}
