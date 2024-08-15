using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevueCraftersTests.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            
        }
        public string Url = BaseUrl;

        public IWebElement MyRevuesNavBarLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[1]/li[1]/a"));
        public IWebElement CreateRevueNavBarLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[1]/li[2]/a"));
        public IWebElement ProfileNavBarLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li[1]/a"));
        public IWebElement LogoutNavBarLink => driver.FindElement(By.XPath("//*[@id=\"navbarSupportedContent\"]/ul[2]/li[2]/a"));

        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }

    }
}
