﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersTest3.Helpers
{
    public static class JSExecutorHelper
    {
        public static void HighLight(IWebElement element, IJavaScriptExecutor js)
        {
            string highLightJS= 
            @"$(arguments[0]).css({ ""border-width"" : ""2px"", ""border-style"" : ""solid"", ""border-color"" : ""red"" });";

            js.ExecuteScript(highLightJS, new object[] { element });
        }
    }
}
