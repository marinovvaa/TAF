using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using WebsiteTest.Pages;
using AutoFramework.Base;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using AutoFramework.Helpers;

namespace WebsiteTest
{
    public class Tests
    {

        string url = "https://anamarinovva.wixsite.com/website-1";

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
        public void TestLogin()
        {
            var homePage = new HomePage();

            var loginPage = homePage.GoToLoginPage();

            loginPage.LogIn("ana.marinovva@gmail.com", "WixPassword_123");

        }

        [Test]
        public void TestShopPage()
        {
            var homePage = new HomePage();

            var shopPage = homePage.GoToShopPage();

            shopPage.ProductLocator("Cup").Click();

            Thread.Sleep(5000);

        }

        [Test]
        public void ExcelTest()
        {
            string fileName = Environment.CurrentDirectory.ToString() + "\\Data\\Login.xlsx";

            ExcelHelper.PopulateCollection(fileName);

            var userName = ExcelHelper.ReadData(1, "UserName");
            var password = ExcelHelper.ReadData(1, "Password");
        }
    }
}