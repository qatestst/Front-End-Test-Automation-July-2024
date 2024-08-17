using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class LoginPage : BasePage
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
            
        }

        public static string Url = BaseUrl + "/User/Login";

        // IWebElement here:
        public IWebElement EmailInputFieldLoginForm => driver.FindElement(By.XPath("//input[@type='email']"));
        public IWebElement PasswordInputFieldLoginForm => driver.FindElement(By.XPath("//input[@type='password']"));
        public IWebElement LoginButtonLoginForm => driver.FindElement(By.XPath("//button[@type='submit']"));

        // Page methods : OpenPage() and Login()

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void Login()
        {
            OpenPage();
            actions.ScrollToElement(LoginButtonLoginForm).Perform();

            EmailInputFieldLoginForm.Clear();
            EmailInputFieldLoginForm.SendKeys("randomUser999@randomMail.com");

            PasswordInputFieldLoginForm.Clear();
            PasswordInputFieldLoginForm.SendKeys("123456");

            LoginButtonLoginForm.Click();
        }

    }
}
