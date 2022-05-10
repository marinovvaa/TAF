using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFramework
{
    public interface IBrowserSession
    {
        /// <summary>
        /// Gets the Selenium IWebDriver
        /// </summary>
        IWebDriver Driver { get; }

        /// <summary>
        /// Gets the session configuration
        /// </summary>
        SessionConfig SessionConfig { get; }

        /// <summary>
        /// Gets the selenium WebDriverWait.
        /// The effective timeout can be obtained from Wait.WaitSeconds.
        /// </summary>
        IWait<IWebDriver> Wait { get; }

        /// <summary>
        /// Closes this instance and returns its duration (excluding user waits).
        /// Always call this method before the BrowserSession is disposed!
        /// </summary>
        /// <returns> Returns the duration (exluding user waits)</returns>
        TimeSpan Close();

        /// <summary>
        /// Does a Driver.FindElement(by) and catches the NoSuchElementException if thrown by Selenium.
        /// In that case it closes the session unless the keepSessionOnFailure is set to true and retuns null.
        /// </summary>
        /// <param name="by">The by.</param>
        /// <param name="keepSessionOnFailure"> if set to true - keep the session on failure</param>
        /// <returns>
        /// Retunrs the element or null if it is not found
        /// </returns>
        IWebElement SafeFindElement(By by, bool keepSessionOnFailure = false);

        /// <summary>
        /// Does a Wait.Until(untilCondition) and catches the WebDriverTimeoutException if thrown by Selenium.
        /// In that case it closes the session unless the keepSessionOnFailure is set to true and retuns null.
        /// </summary>
        /// <typeparam name="TResult"> The type of the result.</typeparam>
        /// <param name="untilCondition">The condition that will be waited for</param>
        /// <param name="keepSessionOnFailure"> if set to true - keep the session on failure</param>
        /// <returns>
        /// Returns the result of the condition or the default value if the wait times out.
        /// </returns>
        TResult SafeWaitUntil<TResult>(Func<IWebDriver, TResult> untilCondition, bool keepSessionOnFailure = false);
        
        /// <summary>
        /// Finds and returns the element specified by the by parameter
        /// Returns null and (by default) closes the session if the element was not found
        /// </summary>
        /// <param name="waitUntilClickable">if set to true will wait for the element to become clickable</param>
        /// <param name="scrollIntoView">if set to true will scroll the element into view first</param>
        /// <param name="keepSessionOnFailure">If set to true will keeep the session open even when the element fas not found</param>
        /// <returns></returns>
        IWebElement SafeGetElement(By by, bool waitUntilClickable = false, bool scrollIntoView = false, bool keepSessionOnFailure = false);

        /// <summary>
        /// Waits for the passed amount of time
        /// </summary>
        /// <param name="waitSec">The time to wait in seconds.</param>
        void UserWait(int waitSec = 0);
    }
}
