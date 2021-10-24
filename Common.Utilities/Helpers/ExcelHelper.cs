using Common.Utilities.Helpers.Interfaces;
using ClosedXML.Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Common.Utilities.Helpers
{
    public class ExcelHelper : IExcelHelper
    {
        /// <summary>
        /// Export given model to excel
        /// </summary>
        /// <param name="model"></param>
        /// <param name="extension"></param>
        /// <param name="additionalCell"> Add extra columns </param>
        /// <returns></returns>
        public byte[] ExportExcel(object model, string extension, Dictionary<int, string> additionalCell = null)
        {
            var modelType = model.GetType();

            var workbook = GetHeaderValue(model, 1);
            //TODO
            var workSheet = workbook.Worksheets.FirstOrDefault();
            var currentRow = 2;
            var cellCount = 1;

            if (typeof(IEnumerable).IsAssignableFrom(modelType))
            {
                IEnumerable oTheList = model as IEnumerable;

                foreach (var item in oTheList)
                {
                    foreach (var prop in item.GetType().GetProperties())
                    {
                        var val = prop.GetValue(item, null);
                        val = GetProperValue(prop.PropertyType, val);

                        if (prop.PropertyType == typeof(string))
                            workSheet.Cell(currentRow, cellCount).Value = string.Concat("'", val);
                        else
                            workSheet.Cell(currentRow, cellCount).Value = val;

                        cellCount += 1;
                    }
                    currentRow += 1;
                    cellCount = 1;
                }
            }
            else if (modelType.IsClass)
            {
                foreach (var prop in modelType.GetProperties())
                {
                    object? val = null;
                    val = prop.GetValue(model, null);
                    val = GetProperValue(prop.PropertyType, val);

                    workSheet.Cell(currentRow, cellCount).Value = val;

                    if (prop.PropertyType == typeof(string))
                        workSheet.Cell(currentRow, cellCount).DataType = XLDataType.Text;

                    cellCount += 1;
                }
            }

            SaveFile(extension);

            using (var stream = new MemoryStream())
            {
                if (additionalCell != null)
                {
                    currentRow += 1;
                    foreach (var customCell in additionalCell)
                    {
                        workSheet.Cell(currentRow, customCell.Key).Value = customCell.Value;
                    }
                }

                workbook.SaveAs(stream);
                var content = stream.ToArray();
                workbook.Dispose();

                return content;
            }
        }

        private XLWorkbook GetHeaderValue(object model, int startingRow)
        {
            var modelType = model.GetType();

            var workBook = new XLWorkbook();

            var worksheet = workBook.Worksheets.Add("Sheet1");
            var currentRow = startingRow;
            var cellCount = 0;

            if (typeof(IEnumerable).IsAssignableFrom(modelType))
            {
                var genericArg = model.GetType().GetGenericArguments()[0];
                foreach (var prop in genericArg.GetProperties())
                {
                    cellCount += 1;
                    worksheet.Cell(currentRow, cellCount).Value = GetDisplayAttrValue(prop);
                }
            }
            else if (modelType.IsClass)
            {
                foreach (var prop in modelType.GetProperties())
                {
                    cellCount += 1;
                    worksheet.Cell(currentRow, cellCount).Value = GetDisplayAttrValue(prop);
                }
            }

            return workBook;

        }

        private string GetDisplayAttrValue(PropertyInfo propInfo)
        {
            var displayAttrList = propInfo.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.DisplayAttribute), false);
            if (displayAttrList != null && displayAttrList.Length == 1)
            {
                var displayAttr = (System.ComponentModel.DataAnnotations.DisplayAttribute)displayAttrList[0];
                return displayAttr.Name;
            }

            return String.Empty;
        }

        private object? GetProperValue(Type propertyType, object? currentValue)
        {
            if (currentValue != null)
            {
                if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    var dateTime = (DateTime)currentValue;

                    if (dateTime != null)
                        currentValue = dateTime.ToShortDateString();
                }
                else if (propertyType == typeof(bool) || propertyType == typeof(bool?))
                {
                    var boolean = (bool?)currentValue;

                    currentValue = boolean.HasValue && boolean.Value ? "Evet" : "Hayır";
                }
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                {
                    var decimalVal = (decimal?)currentValue;

                    if (decimalVal != null)
                        currentValue = decimalVal.Value.ToString("F2");
                }
                else if (propertyType == typeof(Enum) || propertyType.BaseType == typeof(Enum))
                {
                    var enumVal = (Enum)currentValue;

                    if (enumVal != null)
                        currentValue = enumVal.GetAttributeOfType();
                }
            }

            return currentValue;
        }

        private void SaveFile(string extension, string fileName = null)
        {
            string path = Path.Combine("GIVEN_PATH", "Exports");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (string.IsNullOrEmpty(fileName))
                fileName = DateTime.Now.ToString("yyyyMMddHHmm") + extension;

            fileName = Path.GetFileName(fileName);
            FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
            stream.Dispose();

        }
    }
}
