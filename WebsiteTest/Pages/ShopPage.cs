using AutoFramework;
using AutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebsiteTest.Pages
{
    public class ShopPage : BasePage
    {
        public ShopPage(IBrowserSession session) : base(session)
        {
        }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementToBeClickable(By.XPath(productPath)); }
        }
        protected string productPath => "//div[@data-hook = 'product-item-product-details']";

        public IWebElement ProductLocator(string productName) => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath($"{productPath}/h3[text() = '{productName}']")));

        public ProductPage OpenProduct(string productName)
        {
            var product = ProductLocator(productName);
            ProductPage productPage = null;

            if(product != null)
            {
                product.Click();
                productPage = new ProductPage(Session);
            }
            return productPage;
        }
    }
}
