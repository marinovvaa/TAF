using AutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace WebsiteTest.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(bool assertLoaded = false) : base(assertLoaded)
            {
            }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementToBeClickable(By.XPath(loginBtnPath)); }
        }


        protected string loginBtnPath => "//div[@id = 'comp-l23f6gds']/button";

        protected string shopBtnPath => "//div/p[text() = 'Shop']";

        private IWebElement LoginBtn => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(loginBtnPath)));

        private IWebElement ShopBtn => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath(shopBtnPath)));



        /// <summary>
        /// Finds and clicks the Login button at the top right of the page, waits for the LoginPage and returns it
        /// </summary>
        /// <returns></returns>
        public LoginPage GoToLoginPage()
        {
            LoginPage loginPage = null;
            IWebElement loginBtn = LoginBtn;
            if(loginBtn != null)
            {
                //The click does not work from the first time
                loginBtn.Click();
                Thread.Sleep(3000);
                loginBtn.Click();
                Thread.Sleep(3000);


                loginPage = new LoginPage();
            }
            return loginPage;
        }

        public ShopPage GoToShopPage()
        {
            ShopPage shopPage = null;
            IWebElement shopBtn = ShopBtn;
            if (shopBtn != null)
            {
                shopBtn.Click();
              
                shopPage = new ShopPage();
            }
            return shopPage;
        }
    }
}
