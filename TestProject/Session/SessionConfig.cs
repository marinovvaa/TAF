 using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFramework
{
    /// <summary>
    /// Defines the parameters for the BrowserSession
    /// </summary>
    public class SessionConfig
    {
        public string StratUrl { get; set; }    

        /// <summary>
        /// The type of browser where the session will be run.
        /// The browserType determines the concrete type of the used IWebDriver. 
        /// For example for BrowserType.Chrome, Driver will be an implementation of IWebDeiver for the Chrome browser
        /// </summary>
        public BrowserType BrowserType { get; set; }

        public int BrowserWidth { get; set; }   

        public int BrowserHeight { get; set; }  

        /// <summary>
        /// The delay in seconds when calling Wait.Until()
        /// </summary>
        public int WaitSeconds { get; set; }

        /// <summary>
        /// The delay in seconds when calling UserWait()
        /// </summary>
        public int UserWaitSeconds { get; set; }

        public string UserName { get; set; }    

        public string Password { get; set; }

        /// <summary>
        /// Defines the settings ot configuration to be opened.
        /// </summary>
        public SessionConfig(string startUrl, string userName, string password, BrowserType browserType = BrowserType.Chrome,
            int browserWidth = 1200, int browserHeight = 1000, int waitSeconds = 20, int userWaitSeconds = 2)
        {
            StratUrl = startUrl;
            UserName = userName;
            Password = password;    
            BrowserType = browserType;
            BrowserWidth = browserWidth;
            BrowserHeight = browserHeight;
            WaitSeconds = waitSeconds;
            UserWaitSeconds = userWaitSeconds;
        }

        /// <summary>
        /// Tries to read a SessionConfig from the specified .json file.
        /// Will throw FileNotFoundException if the file was not found.
        /// Will throw an IOException if the file could not be read.
        /// Will throw a Newtonsoft.Json.JsonReaderException if the SessionConfig could not be deserialized.
        /// </summary>
        public static SessionConfig ReadFromFile(string filePath)
        {
            SessionConfig result = null; 

            using (var sr = new StreamReader(filePath))
            {
                string configJson = sr.ReadToEnd();
                result = JsonConvert.DeserializeObject<SessionConfig>(configJson);
            }
            return result;
        }
    }
}
