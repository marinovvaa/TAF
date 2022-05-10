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
using AutoFramework;

namespace WebsiteTest
{
    public class Tests : PageObjectTest
    { 

        [SetUp]
        public void Setup()
        {
            Session = new BrowserSession(SessionConfig);
            Session.Driver.Manage().Window.Maximize();
        }

        [TearDown]
        public void AfterEachTest()
        {
            Session.Close();
        }

        [Test]
        public void TestLogin()
        {
            var homePage = new HomePage(Session);

            var loginPage = homePage.GoToLoginPage();

            loginPage.LogIn();

        }

        [Test]
        public void TestShopPage()
        {
            var homePage = new HomePage(Session);

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

        [Test]
        public void LogHelperTest()
        {
            LogHelpers.CreateLogFile();
            LogHelpers.Write("Opened the browser!");
            LogHelpers.Write("Opened the URL");
        }
    }
}