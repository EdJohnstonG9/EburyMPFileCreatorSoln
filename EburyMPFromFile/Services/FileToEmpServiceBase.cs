using EburyMPFromFile.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EburyMPFromFile.Services
{
    public abstract class FileToEmpServiceBase<T> where T: class
    {
        public bool FileValid { get; set; } = false;
        public string FileSearchPattern { get; internal set; }
        public T FileInput { get; set; }

        public abstract IEnumerable<MassPaymentFileModel> GetEmpData();
    }
}
