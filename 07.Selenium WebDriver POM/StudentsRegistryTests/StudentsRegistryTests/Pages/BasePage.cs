using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsRegistryTests.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        
        public virtual string PageUrl { get; }

        //Create Constructor
        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        public IWebElement HomeLink => driver.FindElement(By.XPath("//a[@href='/']"));
        public IWebElement ViewStudentsLink => driver.FindElement(By.LinkText("View Students"));
        public IWebElement AddStudentsLink => driver.FindElement(By.LinkText("Add Student"));
        public IWebElement PageHeading => driver.FindElement(By.CssSelector("body > h1"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(PageUrl);
        }

        public bool IsPageOpen()
        {
            return driver.Url == this.PageUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        public string GetPageHeading()
        {
            return PageHeading.Text;
        }
    }
}
