using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.Core.Support
{
    public static class ZWebDriverWaitExtension
    {
        public static void WithJs(this WebDriverWait wait)
        {
            wait.Until(d =>
            {
                return (d as IJavaScriptExecutor).ExecuteScript("return document.readyState").ToString().Equals("complete");
            });
        }


    }
}
