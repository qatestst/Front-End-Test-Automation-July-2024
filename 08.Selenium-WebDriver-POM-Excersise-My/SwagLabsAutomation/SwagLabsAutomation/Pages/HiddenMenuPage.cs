using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Pages
{
    public class HiddenMenuPage : BasePage
    {
        //Create constructor that inherits base class
        public HiddenMenuPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        // create locators - variable from Selenium class 'By'
        private readonly By hamburgerMenuButton = By.CssSelector(".bm-burger-button");
        private readonly By logoutButton = By.Id("logout_sidebar_link");

        public void ClickHamburgerButton()
        { 
             Click(hamburgerMenuButton);   
        }

        public void ClickLogoutButton()
        { 
            Click(logoutButton);
        }

        public bool IsMenuOpen()
        {
            return FindElement(logoutButton).Displayed;
        }
    }
}
