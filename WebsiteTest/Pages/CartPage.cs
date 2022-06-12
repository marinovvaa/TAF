using AutoFramework;
using AutoFramework.Base;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebsiteTest.Pages
{
    public class CartPage : BasePage
    {
        public CartPage(IBrowserSession session) : base(session)
        {
        }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementExists(By.XPath(checkoutBtnPath)); }
        }

        #region XPaths
        protected string checkoutBtnPath => "//span[text()='Checkout']/ancestor::button";
        #endregion

        #region Elements
        private IWebElement CheckoutBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath(checkoutBtnPath)));

        private IWebElement RemoveItemBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//button[@data-hook='CartItemDataHook.remove']")));

        private IWebElement EmptyCartMessage => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//h3[text() = 'Cart is empty']")));
        #endregion


        #region Methods

        public void RemoveItem()
        {
            var removeBtn = RemoveItemBtn;
            if(removeBtn != null)
            {
                removeBtn.Click();
            }
        }

        public void Checkout()
        {
            var checkoutBtn = CheckoutBtn;
            if (checkoutBtn != null)
            {
                checkoutBtn.Click();
            }
        }

        public string GetMessage()
        {
            var cartMessage = EmptyCartMessage;
            string message = null;
            if (cartMessage != null)
            {
                message = cartMessage.Text;
            }
            return message;
        }

        #endregion

    }
}
