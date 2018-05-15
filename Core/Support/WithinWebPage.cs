using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.Core.Support
{
    /// <summary>
    /// A rewritten version of the obsolete Selenium ExpectedConditions.cs class in Selenium.WebDriver.Support.UI namespace.
    /// </summary>
    public static class WithinWebPage
    {
        public static IWebElement ElementExists(By locator, IWebDriver driver)
        {
            try
            {
                return driver.FindElement(locator);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement ElementIsVisible(By locator, IWebDriver driver)
        {
            try
            {
                var e = driver.FindElement(locator);
                if (e.Displayed)
                {
                    return e;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static IWebElement ElementIsClickable(By locator, IWebDriver driver)
        {
            try
            {
                var e = driver.FindElement(locator);
                if (e.Displayed && e.Enabled)
                {
                    return e;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
