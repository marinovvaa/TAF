using AutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTest.Pages
{
    public class LoginPage : BasePage
    {

        public LoginPage(bool assertLoaded = true) : base(assertLoaded)
        {
        }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementIsVisible(By.XPath(LoginBtnPath)); }
        }


        protected string LoginBtnPath => "//button[text() = 'Log In']";

        //TODO: How to not use new WebdriverWait for each element
        //remove this
        public IWebElement LoginBtn1 => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id = 'comp-l23f6gds']/button/div")));

        public IWebElement LoginBtn2 => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.XPath(LoginBtnPath)));

        public IWebElement LoginWithEmailBtn => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button/span[text() = 'Log in with Email']")));

        public IWebElement EmailInput => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'input_input_emailInput')]")));

        public IWebElement PasswordInput => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'input_input_passwordInput')]")));

        public IWebElement LoginBtn3 => new WebDriverWait(DriverContext.Driver, TimeSpan.FromSeconds(15)).Until(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@id,'okButton')]")));


        public void LogIn(string email, string password)
        {
            LoginBtn2.Click();
            LoginWithEmailBtn.Click();
            EmailInput.SendKeys(email);
            PasswordInput.SendKeys(password);
            LoginBtn3.Click();
        }
    }
}
