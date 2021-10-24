using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Utilities.Models
{
    public class DummyExcelModel
    {
        public string Name { get; set; }
        public int Age{ get; set; }
        public string Profession{ get; set; }
    }

    public enum Fruits
    {
        [Description("Apple")]
        Apple = 1,
        [Description("Orange")]
        Orange = 2,
        [Description("Strawberry")]
        Strawberry = 3
    }
}
