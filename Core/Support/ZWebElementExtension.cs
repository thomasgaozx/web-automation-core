using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.Core.Support
{
    public static class ZWebElementExtension
    {
        public static void ClearAndSendKeys(this IWebElement element, string content)
        {
            element.Clear();
            element.SendKeys(content);
        }
    }
}
