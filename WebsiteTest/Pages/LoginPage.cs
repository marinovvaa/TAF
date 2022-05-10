using AutoFramework;
using AutoFramework.Base;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteTest.Pages
{
    public class LoginPage : BasePage
    {

        public LoginPage(IBrowserSession session) : base(session)
        {
        }

        public override Func<IWebDriver, IWebElement> LastLoadedElementCondition
        {
            get { return ExpectedConditions.ElementIsVisible(By.XPath(LoginBtnPath)); }
        }


        protected string LoginBtnPath => "//button[text() = 'Log In']";

        //TODO: How to not use new WebdriverWait for each element
        //remove this
        public IWebElement LoginBtn1 => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//div[@id = 'comp-l23f6gds']/button/div")));

        public IWebElement LoginBtn2 => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath(LoginBtnPath)));

        public IWebElement LoginWithEmailBtn => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//button/span[text() = 'Log in with Email']")));

        public IWebElement EmailInput => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'input_input_emailInput')]")));

        public IWebElement PasswordInput => Session.SafeWaitUntil(ExpectedConditions.ElementIsVisible(By.XPath("//*[contains(@id,'input_input_passwordInput')]")));

        public IWebElement LoginBtn3 => Session.SafeWaitUntil(ExpectedConditions.ElementToBeClickable(By.XPath("//*[contains(@id,'okButton')]")));


        /// <summary>
        /// Enters the user credentials and clicks the login buttons.
        /// If the user credentials are not passed as parameters they are taken from the session config.
        /// </summary>
        public void LogIn([Optional]string email,[Optional] string password)
        {
            LoginBtn2.Click();
            LoginWithEmailBtn.Click();

            if(email == null)
            {
                email = Session.SessionConfig.UserName;
            }
            if (password == null)
            {
                password = Session.SessionConfig.Password;
            }

            EmailInput.Clear();
            EmailInput.SendKeys(email);

            PasswordInput.Clear();
            PasswordInput.SendKeys(password);

            LoginBtn3.Click();
        }
    }
}
