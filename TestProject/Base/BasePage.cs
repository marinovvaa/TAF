using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace AutoFramework.Base
{
    /// <summary>
    /// Base class for all page object classes
    /// </summary>
    public abstract class BasePage
    {

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
        public BasePage(bool assertLoaded = true)
        {
            _ = DriverContext.Driver;
            CheckLoaded(assertLoaded);
        }
        #endregion

        #region methods
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
                    var lastLoadedElement = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(LastLoadedElementCondition);
                    loaded = true;
                }
                catch(WebDriverTimeoutException)
                {
                    if(assertLoaded)
                    {
                        DriverContext.Driver.Close();
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

