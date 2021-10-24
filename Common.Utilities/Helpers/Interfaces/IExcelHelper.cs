using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utilities.Helpers.Interfaces
{
    public interface IExcelHelper
    {
        public byte[] ExportExcel(object model, string extension, Dictionary<int, string> additionalCell = null);
    }
}
