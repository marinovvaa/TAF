using AutoFramework.Base;
using AutoFramework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTest
{
    public class TestHtmlHelper
    {
        string url = Environment.CurrentDirectory.ToString() + "\\Data\\TestHtmlTable.html";

        //Remove from here!!!
        public void OpenBrowser(BrowserType browserType = BrowserType.Chrome)
        {
            switch (browserType)
            {
                case BrowserType.InternetExplorer:
                    DriverContext.Driver = new InternetExplorerDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.FireFox:
                    DriverContext.Driver = new FirefoxDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
                case BrowserType.Chrome:
                    DriverContext.Driver = new ChromeDriver();
                    DriverContext.Browser = new Browser(DriverContext.Driver);
                    break;
            }
        }

        [SetUp]
        public void Setup()
        {
            OpenBrowser(BrowserType.Chrome);
            DriverContext.Browser.GoToUrl(url);
        }

        [TearDown]
        public void AfterEachTest()
        {
            DriverContext.Driver.Close();
        }

        [Test]
        public void TestHtmlHelpersExample()
        {
            //var table = new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementExists(By.XPath("//table")));
            var table = DriverContext.Driver.FindElement(By.XPath("//table"));
            HtmlTableHelper.ReadTable(table);
            HtmlTableHelper.PerformActionOnCell("3", "Name", "Ana", "gitHub");

            var pageTitle = DriverContext.Driver.Title;

            Assert.AreEqual("GitHub - marinovvaa/TAF", pageTitle);


        }
    }
}
