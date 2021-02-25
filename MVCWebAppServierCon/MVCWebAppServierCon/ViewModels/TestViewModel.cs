using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MVCWebAppServierCon.Models;
namespace MVCWebAppServierCon.ViewModels
{
    public class TestViewModel
    {   
            public TestClass Test { get; set; }
            public Test2Class  Test2 { get; set; }

            public List<TestClass> TestLst { get; set; }
            public List<Test2Class>  Test2Lst { get; set; }
     
        }
    }



