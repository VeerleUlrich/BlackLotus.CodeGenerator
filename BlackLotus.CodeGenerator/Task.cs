using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackLotus.CodeGenerator
{
    internal class Task
    {
        [AllowNull, Column]
        public int MyProperty { get; set; }
        [Column]
        public int MyProperty2 { get; set; }
        public void TestMethod() { }
        public void SecondTestMethod() { }
    }

    //[Obsolete]
    //public class SecondTestClass
    //{
    //    [AllowNull]
    //    public int SecondProperty { get; set; }

    //    public void TestMethod() { }
    //    public void SecondTestMethod() { }
    //}
}
