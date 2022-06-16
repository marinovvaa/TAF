using NUnit.Framework;
using System;
using WebsiteTest.Pages;
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
        public void TearDown()
        {
            Session.Close();
        }      

        [Test]
        public void CheckProductName()
        {
            var homePage = new HomePage(Session);

            var shopPage = homePage.GoToShopPage();

            var productPage = shopPage.OpenProduct("Cup");

            var productTitle = productPage.GetProductTitle();

            Assert.AreEqual("Cup", productTitle);

        }

        [Test]
        public void TestLogin()
        {
            var homePage = new HomePage(Session);

            var loginPage = homePage.GoToLoginPage();

            loginPage.LogIn();

        }

        [Test]
        public void CheckProductDescription()
        {
            var homePage = new HomePage(Session);

            var shopPage = homePage.GoToShopPage();

            var productPage = shopPage.OpenProduct("Shoes");

            var productDescription = productPage.GetProductDescription();

            Assert.AreEqual("Leather mans shoes .", productDescription);
        }

        [Test]
        public void CheckProductQuantity()
        {
            var homePage = new HomePage(Session);

            var shopPage = homePage.GoToShopPage();

            var productPage = shopPage.OpenProduct("Shoes");

            productPage.IncreaseQuantity(2);

            var productQuantity = productPage.GetProductQuantity();

            Assert.AreEqual("3", productQuantity);

            productPage.DecreaseQuantity(1);

            productQuantity = productPage.GetProductQuantity();

            Assert.AreEqual("2", productQuantity);

        }

        [Test]
        public void CheckSwitchingProducts()
        {
            var homePage = new HomePage(Session);

            var shopPage = homePage.GoToShopPage();

            Session.UserWait(2);

            var productPage = shopPage.OpenProduct("Shoes");

            var productPage1 = productPage.GoToNextProduct();

            Session.UserWait(1);

            var title = productPage1.GetProductTitle();

            Assert.AreEqual("Glasses", title);
        }

        [Test]
        public void CheckCart()
        {
            var homePage = new HomePage(Session);

            var shopPage = homePage.GoToShopPage();

            Session.UserWait(2);

            var productPage = shopPage.OpenProduct("Cup");

            var cartPopup = productPage.AddToCart();

            var total = cartPopup.GetCartTotal();

            Assert.AreEqual("5,39 ыт.", total);

            var cartPage = cartPopup.ViewCart();

            cartPage.RemoveItem();

            var message = cartPage.GetMessage();

            Assert.AreEqual("Cart is empty", message);

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
            LogHelper.CreateLogFile();
            LogHelper.Write("Opened the browser!");
            LogHelper.Write("Opened the URL");
        }
    }
}