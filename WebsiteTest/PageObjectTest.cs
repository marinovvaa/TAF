using AutoFramework;
using System;

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
                return SessionConfig.ReadFromFile(Environment.CurrentDirectory.ToString() + "//SessionConfig.json");
            }
        }
    }
}
