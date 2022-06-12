using AutoFramework;
using AutoFramework.Base;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebsiteTest.Pages
{
    public class CartPopupPage : BasePage
    {
        public CartPopupPage(IBrowserSession session) : base(session)
        {
        }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementIsVisible(By.XPath("//div[@class = 'product-quantity-container']")); }
        }

        protected string cartBtnPath => "//a[@id='widget-view-cart-button']";

        private IWebElement ViewCartBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath(cartBtnPath)));

        private IWebElement TotalPrice => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//div[@data-hook ='cart-widget-total']")));

        private IWebElement CloseBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@id='cart-widget-close']")));


        public string GetCartTotal()
        {
            var cartTotal = TotalPrice;
            string total = null;
            if (cartTotal != null)
            {
                total = cartTotal.Text;
            }
            return total;
        }

        public CartPage ViewCart()
        {
            var viewCartBtn = ViewCartBtn;
            CartPage cartPage = null;
            if (viewCartBtn != null)
            {
                viewCartBtn.Click();
                cartPage = new CartPage(Session);
            }
            return cartPage;
        }

        public void CloseCart()
        {
            var closeBtn = CloseBtn;
            if(closeBtn != null)
            {
                closeBtn.Click();
            }
        }
    }
}
