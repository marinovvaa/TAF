using AutoFramework;

namespace WebsiteTest
{
    /// <summary>
    /// Base class for the test classes in WebsiteTest
    /// Defines the session config
    /// </summary>
    public abstract class PageObjectTest
    {
        protected BrowserSession Session { get; set; }

        protected SessionConfig SessionConfig
        {
            get
            {
                return new SessionConfig("https://anamarinovva.wixsite.com/website-1", "ana.marinovva@gmail.com", "WixPassword_123");
            }
        }
    }
}
