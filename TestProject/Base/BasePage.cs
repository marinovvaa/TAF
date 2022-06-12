using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace AutoFramework.Base
{
    /// <summary>
    /// Base class for all page object classes
    /// </summary>
    public abstract class BasePage
    {
        public IBrowserSession Session { get; set; }    

        public bool IsLoaded { get; private set; }

        /// <summary>
        /// When overridden in a derived class, defines a function for finding an IWebElement
        /// that, when successful, indicates that the page has been loaded completely
        /// </summary>
        public abstract Func<IWebDriver,IWebElement> LastLoadedElementCondition { get; }

        #region ctor
        /// <summary>
        /// Base constructor for all derived page object classes.
        /// By default, checks if the corresponding page has been loaded, using the LastLoadedElementCondition.
        /// This check can be turned off by setting the checkIfLoaded parameter to false
        /// </summary>
        public BasePage(IBrowserSession session, bool assertLoaded = true)
        {
            Session = session;
            CheckLoaded(assertLoaded);
        }
        #endregion

        #region methods

        /// <summary>
        /// Hover onto an element by finding it by xpath that is passed
        /// </summary>
        public void HoverElement(string xPath)
        {
            IWebElement element = Session.SafeGetElement(By.XPath(xPath));

            //Creating object of an Actions class
            Actions action = new Actions(Session.Driver);

            //Performing the mouse hover action on the target element.
            action.MoveToElement(element);
        }
        public IWebElement GetItemByText(IReadOnlyCollection<IWebElement> items, string text)
        {
            IWebElement result = null;
            foreach (IWebElement item in items)
            {
                if (item.Text == text)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }

        /// <summary>
        /// Tries to select the option with the given text from the options of the passed select.
        /// When successful returns the selected option as IWebElement.
        /// Otherwise closes the session and returns null.
        /// </summary>    
        public IWebElement SelectOption(IWebElement select, string optionText)
        {
            IWebElement result = null;
            if(select != null)
            {
                var options = select.FindElements(By.XPath(".//option"));
                result = GetItemByText(options, optionText);
                if(result != null)
                {
                    result.Click();
                }
            }
            if(result == null)
            {
                Session.Close();
            }
            return result;
        }

        /// <summary>
        /// Performs right click on the passed element
        /// </summary>
       public void RightClickElement(IWebElement element)
        {
            var actions = new Actions(Session.Driver);
            actions.ContextClick(element).Build();
            actions.Perform();
        }
        /// <summary>
        /// Preforms a double click on the passed element
        /// </summary>
        public void DoubleClickElement(IWebElement element)
        {
            var actions = new Actions(Session.Driver);
            actions.DoubleClick(element).Build();
            actions.Perform();
        }

        /// <summary>
        /// After an initial wait of initialWaiSecond (1 second by default), tries to find the 
        /// element with the passed locator xpath. If the element is found waits until the element is not found
        /// by searching for it once per second.
        /// By deafult the method will stop waiting after 60 seconds.(maxWaitSeconds)
        /// </summary>      
        public bool WaitUntilNotFoundAnymore(string locatorPath, int initialWaitSeconds = 1, int maxWaitSeconds = 60)
        {
            bool result = true;
            Session.UserWait(initialWaitSeconds);
            var totalWaitSeconds = initialWaitSeconds;
            IWebElement element = Session.SafeFindElement(By.XPath(locatorPath), true);
            if(element != null)
            {
                result = false;
                do
                {
                    Session.UserWait(1);
                    totalWaitSeconds++;
                    element = Session.SafeFindElement(By.XPath(locatorPath), true);
                }
                while((element != null) && (totalWaitSeconds < maxWaitSeconds));

                result = (element == null);
            }

            return result;
        }
          
        /// <summary>
        /// Checks if the page of this page object has been loaded. This is the case if the LastLoadedElementCondition
        /// is not null and if it becomes true within the WaitSecondsTimeout. the result of the check is stored in the 
        /// IsLoaded property of PageBase.
        /// If the assertLoaded parameter is set to true, the method will additionally Assert this fact. If the assertin
        /// fails then the calling test will fail. Fir this case, CheckLoaded() will also close the Session (so that the browser instance will be disposed of).
        /// </summary>
        public void CheckLoaded(bool assertLoaded)
        {
            bool loaded = false;
            if(LastLoadedElementCondition != null)
            {
                try
                {
                    var lastLoadedElement = Session.Wait.Until(LastLoadedElementCondition);
                    loaded = true;
                }
                catch(WebDriverTimeoutException)
                {
                    if(assertLoaded)
                    {
                        Session.Close();
                    }
                }
                if(!loaded && assertLoaded)
                {
                    throw new TimeoutException(GetType().Name + "was not loaded within the WaitSecondsTimeout. Session Was closed. Check LastLoadedElementCondition");
                }
            }
            IsLoaded = loaded;
        }
        #endregion
    }
}

