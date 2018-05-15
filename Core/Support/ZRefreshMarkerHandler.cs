using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using WebAutomation.Core.Base;

namespace WebAutomation.Core.Support
{
    public class ZRefreshMarkerHandler : BaseController
    {
        public const string RefreshMarker = "unrefreshed";

        public void AddRefreshMarker()
        {
            ((IJavaScriptExecutor)driver).ExecuteScript(
                $"document.querySelector('body').className+='{RefreshMarker}'");
        }

        public void WaitUntilInvisibilityOfRefreshMarker()
        {
            wait.Until(d =>
            {
                return !d.FindElements(By.ClassName(RefreshMarker)).Any();
            });
        }

        public ZRefreshMarkerHandler(BaseController controller) : base(controller) { }

        public ZRefreshMarkerHandler(IWebDriver driver, WebDriverWait wait) : base(driver, wait) { }
    }
}
