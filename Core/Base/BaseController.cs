using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAutomation.Core.Base
{
    public abstract class BaseController
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        #region Life Cycle 

        public BaseController(IWebDriver driver, WebDriverWait wait)
        {
            this.driver = driver;
            this.wait = wait;
        }

        public BaseController(BaseController controller)
        {
            this.driver = controller.driver;
            this.wait = controller.wait;
        }

        public BaseController()
        {
            driver = null;
            wait = null;
        }

        #endregion
    }
}
