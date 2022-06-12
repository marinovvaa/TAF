using AutoFramework;
using AutoFramework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

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
