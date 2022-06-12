using AutoFramework;
using AutoFramework.Base;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using System;

namespace WebsiteTest.Pages
{
    public class ProductPage : BasePage
    {
        public ProductPage(IBrowserSession session) : base(session)
        {
        }
        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementExists(By.XPath(addToCartBtnPath)); }
        }

        #region XPaths
        protected string nextBtnPath => "//a[@rel ='next']";

        protected string addToCartBtnPath => "//span[text() = 'Add to Cart']/ancestor::button";

        protected string quantityInputPath => "//input[@aria-label = 'Quantity']";
        #endregion

        #region Elements

        private IWebElement NextBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath(nextBtnPath)));

        private IWebElement ProductTitle => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//h1[@data-hook ='product-title']")));
       
        private IWebElement ProductDescription => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//pre[@data-hook='description']")));

        private IWebElement ProductQuantity => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath(quantityInputPath)));

        private IWebElement IncreaseQuantityButton => Session.SafeGetElement(By.XPath("//span[@data-hook = 'number-input-spinner-up-arrow']"));

        private IWebElement DecreaseQuantityButton => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//span[@data-hook = 'number-input-spinner-down-arrow']")));

        private IWebElement AddToCartButton => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//span[text() = 'Add to Cart']/ancestor::button")));


        #endregion

        #region Methods
        public string GetProductTitle()
        {
            var productTitle = ProductTitle;
            string title = null;
            if(productTitle != null)
                {
                title = productTitle.Text;
                }
            return title;
        }

        public string GetProductDescription()
        {
            var productDescription = ProductDescription;
            string description = null;
            if (productDescription != null)
            {
                description = productDescription.Text;
            }
            return description;
        }

        public string GetProductQuantity()
        {
            var productQuantity = ProductQuantity;
            string quantity = null;
            if (productQuantity != null)
            {
                quantity = productQuantity.GetAttribute("value");
            }
            return quantity;
        }
        public ProductPage GoToNextProduct()
        {
            var nextBtn = NextBtn;
            ProductPage nextProduct = null;
            if(nextBtn != null)
            {
                NextBtn.Click();
                nextProduct = new ProductPage(Session);
            }
            return nextProduct;
        }

        public void IncreaseQuantity(int by)
        {
            var increaseBtn = IncreaseQuantityButton;
            int i = 0;
            if (increaseBtn != null)
            {
                HoverElement("//span[@data-hook = 'number-input-spinner-up-arrow']");
                //by+1 is used because the first click does not work
                while (i < by+1)
                {
                    increaseBtn.Click();
                    Session.UserWait(1);
                    i++;
                }
            }
        }

        public void DecreaseQuantity(int by)
        {
            var decreaseBtn = DecreaseQuantityButton;
            int i = 0;
            if (decreaseBtn != null)
            {
                HoverElement("//span[@data-hook = 'number-input-spinner-up-arrow']");
                while (i < by)
                {
                    decreaseBtn.Click();
                    Session.UserWait(1);
                    i++;
                }
            }
        }

        public CartPopupPage AddToCart()
        {
            var addToCartBtn = AddToCartButton;
            CartPopupPage cartPopupPage = null;
            if(addToCartBtn != null)
            {
                addToCartBtn.Click();
                Session.Driver.SwitchTo().Frame(1);
                cartPopupPage = new CartPopupPage(Session);
            }
            return cartPopupPage;
        }
        #endregion
    }
}
