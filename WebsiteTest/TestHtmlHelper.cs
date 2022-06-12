using AutoFramework;
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
    public class TestHtmlHelper : PageObjectTest
    {
        string url = Environment.CurrentDirectory.ToString() + "\\Data\\TestHtmlTable.html";

    

        [SetUp]
        public void Setup()
        {
            Session = new BrowserSession(SessionConfig);
            Session.Driver.Manage().Window.Maximize();
            Session.Driver.Navigate().GoToUrl(url);
        }

        [TearDown]
        public void AfterEachTest()
        {
            Session.Close();
        }

        [Test]
        public void TestHtmlHelpersExample()
        {
            var table = Session.SafeGetElement(By.XPath("//table"));
            HtmlTableHelper.ReadTable(table);
            HtmlTableHelper.PerformActionOnCell("3", "Name", "Ana", "gitHub");

            var pageTitle = Session.Driver.Title;

            Assert.AreEqual("GitHub - marinovvaa/TAF", pageTitle);


        }
    }
}
