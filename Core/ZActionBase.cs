using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebAutomation.Core.Base;
using WebAutomation.Core.Support;

namespace WebAutomation.Core
{
    public abstract class ZActionBase : BaseController
    {

        #region Private Helper Methods
        /// <summary>
        /// Locates the main program in the fixture
        /// </summary>
        /// <returns></returns>
        private MethodInfo LocateZActionMethod()
        {
            Type type = this.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic);

            return methods.FirstOrDefault(m => Attribute.IsDefined(m, typeof(ZActionAttribute)));
        }

        private void CallMethod()
        {
            MethodInfo main = LocateZActionMethod();

            if (main == null)
            {
                throw new Exception("No methods with attribute ZAction is detected.");
            }

            main.Invoke(this, null);
        }

        private async Task CallMethodAsync()
        {
            MethodInfo main = LocateZActionMethod();

            if (main == null)
            {
                throw new Exception("No methods with attribute ZAction is detected.");
            }

            await (Task)main.Invoke(this, null);
        }

        private void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }

        private void HandleException(Exception e)
        {
            ZDebugUtil.PrintError(e);
            QuitDriver();
        }

        private void PressEnterKeyToContinue()
        {
            Console.WriteLine("Press Enter key to quit ...");
            Console.ReadLine();
        }

        #endregion

        #region SetUp Helper

        /// <summary>
        /// Set up driver and wait
        /// </summary>
        protected void LaunchDriver(string downloadPath = @"C:\Users\Owner\Downloads")
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            options.AddUserProfilePreference("download.default_directory", downloadPath);
            options.AddUserProfilePreference("disable-popup-blocking", "true");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        #endregion

        #region Syntacyical Shortcut

        protected IWebElement Clickable(By locator)
        {
            return wait.Until(d => WithinWebPage.ElementIsClickable(locator, d));
        }

        protected IWebElement Visible(By locator)
        {
            return wait.Until(d => WithinWebPage.ElementIsVisible(locator,d));
        }

        protected ZActionBase Click(By locator)
        {
            Clickable(locator).Click();
            return this;
        }

        protected ZActionBase SendKeys(By locator, string content)
        {
            Clickable(locator).SendKeys(content);
            return this;
        }

        protected ZActionBase ClearAndSendKeys(By locator, string content)
        {
            Clickable(locator).ClearAndSendKeys(content);
            return this;
        }

        protected object JS(string js, params object[] args)
        {
            return (driver as IJavaScriptExecutor).ExecuteScript(js, args);
        }

        protected void SelectOption(IWebElement menu, string containedText)
        {
            var options = menu.FindElements(By.TagName("option"));
            IWebElement option = options.FirstOrDefault(o => o.Text.ToLower().Contains(containedText.ToLower()));
            if (option == null)
                throw new Exception($"Cannot find option that contains '{containedText}'");
            string optionKey = option.GetAttribute("value");
            JS($"arguments[0].value='{optionKey}'", menu);
        }

        #endregion

        /// <summary>
        /// Invoke any methods with attribute [ZAction] in the Main program.
        /// </summary>
        public void Run()
        {
            try
            {
                CallMethod();
            }
            catch (Exception e) { HandleException(e); }
            finally
            {
                QuitDriver();
                PressEnterKeyToContinue();
            }
        }

        public async Task RunAsync()
        {
            try
            {
                await CallMethodAsync();
            }
            catch (Exception e) { HandleException(e); }
            finally
            {
                QuitDriver();
                PressEnterKeyToContinue();
            }
        }

    }
}
