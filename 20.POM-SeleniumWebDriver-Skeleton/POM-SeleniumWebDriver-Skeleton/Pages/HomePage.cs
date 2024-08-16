using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM_SeleniumWebDriver_Skeleton.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver) : base(driver)
        {
            
        }

        public string Url = BaseUrl;




        //Page Methods here
        public void OpenPage()
        {
            driver.Navigate().GoToUrl(Url);
        }


    }
}
