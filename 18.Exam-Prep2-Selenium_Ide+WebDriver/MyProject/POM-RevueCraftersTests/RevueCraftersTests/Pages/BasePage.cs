using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;

namespace RevueCraftersTests.Pages
{
   
    public class BasePage
    {
        protected IWebDriver driver;

        protected WebDriverWait wait;

        protected Actions actions;

        protected static string BaseUrl = "https://d3s5nxhwblsjbi.cloudfront.net";

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            this.actions = new Actions(driver);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement NavBarLoginLink => driver.FindElement(By.XPath("//a[@class='nav-link text-dark'][@href='/Users/Login#loginForm']"));
        public IWebElement NavBarRegisterLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li[1]/a"));
        public IWebElement NavBarHomeLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[1]/li[1]/a"));
        public IWebElement NavBarAboutLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[1]/li[2]/a"));
        public IWebElement NavBarServicesLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[1]/li[3]/a"));






    }
}
