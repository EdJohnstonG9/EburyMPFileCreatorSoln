using CsvHelper;

using EburyMPFromFile.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Services
{
    public abstract class FileToEmpServiceBase<FileModel> where FileModel : class
    {
        public bool FileValid { get; set; } = false;
        public string FileSearchPattern { get; internal set; } = string.Empty;
        public FileModel? FileInput { get; set; }
        public string ErrorText = string.Empty;

        public virtual bool ReadInputFile(string file, string path)
        {
            FileValid = false;
            ErrorText = string.Empty;
            var fi = new FileInfo(Path.Combine(path, file));
            if (!fi.Exists)
            {
                ErrorText = $"{nameof(ReadInputFile)}\tFile Not Found: {fi.FullName}";
                throw new ArgumentException(ErrorText);
            }
            FileValid = GetModelFromFile(fi.FullName);
            return FileValid;
        }

        protected abstract bool GetModelFromFile(string filePath);

        public virtual IEnumerable<MassPaymentFileModel> ConvertToEmpData()
        {
            if (!FileValid || FileInput == null)
            {
                throw new ArgumentException($"{nameof(ConvertToEmpData)}\tCannot convert, no or invalid data");
            }
            return GetEmpDataFromModel(FileInput);
        }
        protected abstract IEnumerable<MassPaymentFileModel> GetEmpDataFromModel(FileModel fileInput);

    }
}
