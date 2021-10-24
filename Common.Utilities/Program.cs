using Common.Utilities.Helpers;
using Common.Utilities.Models;
using System;
using System.Collections.Generic;

namespace GenericExcelExport
{
    class Program
    {

        static void Main(string[] args)
        {
            List<DummyExcelModel> excelModels = new List<DummyExcelModel>()
            {
                new DummyExcelModel{ Name="Steve",Age=25,Profession="Developer" },
                new DummyExcelModel{ Name="John",Age=62,Profession="Project Manager" },
                new DummyExcelModel{ Name="Karen",Age=19,Profession="Analyst" }
            };

            //Initialize from interface
            new ExcelHelper().ExportExcel(excelModels, "xls");


            var enums = EnumHelper.GetEnumList<Fruits>();
            var appleEnum = EnumHelper.GetAttributeOfType(Fruits.Apple);


        }
    }
}
