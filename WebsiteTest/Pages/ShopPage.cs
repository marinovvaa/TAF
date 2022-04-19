using AutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebsiteTest.Pages
{
    public class ShopPage : BasePage
    {
        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementToBeClickable(By.XPath(productPath)); }
        }
        protected string productPath => "//div[@data-hook = 'product-item-product-details']";

        public IWebElement ProductLocator(string productName) => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).
            Until(ExpectedConditions.ElementToBeClickable(By.XPath($"{productPath}/h3[text() = '{productName}']")));

    }
}
