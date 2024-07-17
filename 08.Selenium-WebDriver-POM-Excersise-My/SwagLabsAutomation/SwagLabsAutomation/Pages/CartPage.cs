using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Pages
{
    public class CartPage : BasePage
    {
        private readonly By cartItem = By.CssSelector(".cart_item");
        private readonly By checkoutButton = By.CssSelector("#checkout");

        public CartPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }


        public bool IsCartItemDisplayed()
        {
            return FindElement(cartItem).Displayed;
        }

        public void ClickCheckout()
        {
            Click(checkoutButton);
        }
    }
}
