using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Pages
{
    public class InventoryPage : BasePage
    {
        protected readonly By cartLink = By.CssSelector(".shopping_cart_link");
        protected readonly By productsPageTitle = By.ClassName("title");
        protected readonly By productItems = By.CssSelector(".inventory_item");

        public InventoryPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public void AddToCartByIndex(int itemIndex)
        {
            var itemByIndexButton = By.XPath($"//div[@class='inventory_list']//div[@class='inventory_item'][{itemIndex}]//button");
            
            Click(itemByIndexButton);
        }

        public void AddToCartByName(string name)
        {
            var itemByNameButton = By.XPath($"//div[text()='{name}']/ancestor::div[@class='inventory_item_description']//button");

            Click(itemByNameButton);
        }

        public void ClickCartLink()
        {
            Click(cartLink);
        }

        public bool IsInventoryPageDisplayed()
        {
            return FindElements(productItems).Any();
        }

        public bool IsInventoryPageLoaded()
        {
            return GetText(productsPageTitle) == "Products" && driver.Url.Contains("inventory.html");
        }



    }
}
