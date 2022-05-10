using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AutoFramework
{
    /// <summary>
    /// Base class for coded UI test sessions based on Selenium WebDriver.
    /// Abstracts away some implementation details of the WebDriver, for example auto-creates
    /// a proper instance of IWebDriver for the BrowserType passed to the constructor by loading
    /// the corresponding driver (.exe) that is part of the framework. This means that such drivers
    /// do not have to be installed manually on the machine where the tests are run.
    /// </summary>
    public class BrowserSession : IBrowserSession
    {
        /// <summary>
        /// Gets the Selenium IWebDriver instance that was created when this BrowserSession was created.
        /// </summary>
        public IWebDriver Driver { get; }

        /// <summary>
        /// Gets the Selenium WebDriverWait that was created when this BrowserSession was created.
        /// The effective timeout ot Wait.Until() can be obtained from the WaitSeconds property
        /// </summary>
        public IWait<IWebDriver> Wait { get; }

        public SessionConfig SessionConfig { get; }

        private DateTime startTime;
        private int totalUserWaitSeconds;

        public BrowserSession(SessionConfig sessionConfig)
        {
            startTime = DateTime.Now;
            totalUserWaitSeconds = 0;

            SessionConfig = sessionConfig;

            var driverPath = GetDriverPath(SessionConfig.BrowserType);

            switch (SessionConfig.BrowserType)
            {
                case BrowserType.Chrome:
                    Driver = new ChromeDriver(driverPath);
                    break;
                case BrowserType.Firefox:
                    Driver = new FirefoxDriver(driverPath);
                    break;
                case BrowserType.InternetExplorer:
                    Driver = new InternetExplorerDriver(driverPath);
                    break;
                case BrowserType.Edge:
                    Driver = new EdgeDriver(driverPath);
                    break;              
            }
            Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(SessionConfig.WaitSeconds));
            Driver.Manage().Window.Size = new System.Drawing.Size(SessionConfig.BrowserWidth, SessionConfig.BrowserHeight);
            Driver.Url = SessionConfig.StratUrl;
        }

        private string GetDriverPath(BrowserType browserType)
        {
            string result = "";
            string outputDir = Environment.CurrentDirectory.ToString();
            string relPath = "\\SeleniumDrivers\\";
            string driverDir = browserType.ToString(); //e.g "Chrome"            
            result = outputDir + relPath + driverDir;
            return result;
        }

        /// <summary>
        /// When UserWaitSeconds has been seto to a values > 0 (in the constructor)
        /// or when a wait > 0 was specified as a parameter, a call to this method
        /// will pause execution for that time span
        /// </summary>
        public void UserWait(int waitSec = 0)
        {
            int wait = 0;
            if(waitSec > 0)
            {
                wait = waitSec;
            }
            else
            {
                wait = SessionConfig.UserWaitSeconds;
            }
            Thread.Sleep(wait * 1000);
            totalUserWaitSeconds += wait;
        }

        /// <summary>
        /// Does a Wait.Until(untilCondition) and catches the WebDriverTimeoutException, if thrown by Selenium.
        /// In that case, closes the session unless the keepSessionOnFailure is set to true and returns the default value.
        /// </summary>       
        public TResult SafeWaitUntil<TResult>(Func<IWebDriver, TResult> untilCondition, bool keepSessionOnFailure = false)
        {
            TResult result = default(TResult);
            try
            {
                result = Wait.Until(untilCondition);
            }
            catch(WebDriverTimeoutException)
            {
                if(!keepSessionOnFailure)
                {
                    Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Finds and returns the element specified by the by parameter.
        /// If the element must be scrolled into view first, set scroll info view to true.
        /// If you intend to click the returned element, set waitUntilClickable to be true.
        /// If you want to keep the session open, even when the result is null, set keep session on failure to be true.
        /// Returns null and (by default) closes the session if the element was not found.
        /// </summary>        
        public IWebElement SafeGetElement(By by, bool waitUntilClickable = false, bool scrollIntoView = false, bool keepSessionOnFailure = false)
        {
            IWebElement result = SafeFindElement(by, keepSessionOnFailure);

            if(result != null)
            {
                if(scrollIntoView)
                {
                    ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", result);
                }
                if(waitUntilClickable)
                {
                    result = SafeWaitUntil(ExpectedConditions.ElementToBeClickable(by), keepSessionOnFailure);
                }
            }
            return result;
        }

        /// <summary>
        /// Does a Driver.FindElement(by) and catches the NoSuchElementException, if thrown by selenium.
        /// In that case, closes the session unless keepSessionOnFailure is set to true and returns null.
        /// </summary>      
        public IWebElement SafeFindElement(By by, bool keepSessionOnFailure = false)
        {
            IWebElement result = null;
            try
            {
                result = Driver.FindElement(by);
            }
            catch (NoSuchElementException)
            {
                if(!keepSessionOnFailure)
                {
                    Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Closes the current BrowserSession and retunrs its duration(not counting userWaits).
        /// </summary>
        /// <returns></returns>
        public TimeSpan Close()
        {
            Driver.Close();
            Driver.Quit();
            DateTime endTime = DateTime.Now;
            TimeSpan userWait = TimeSpan.FromSeconds(totalUserWaitSeconds);
            return endTime - startTime - userWait;

        }
    
    }
}
