using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Tests
{
    public class InventoryTests :BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
        }

        [Test]
        public void TestInventoryDisplay() 
        {
            Assert.That(inventoryPage.IsInventoryPageDisplayed(), Is.True, "Inventory page has no items displayed");
        }

        [Test]
        public void TestAddToCartByIndex()
        {
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();

            Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "Cart item was not added in the cart");
        }

        [Test]
        public void TestAddToCartByName()
        {
            inventoryPage.AddToCartByName("Sauce Labs Fleece Jacket");
            inventoryPage.ClickCartLink();

            Assert.That(cartPage.IsCartItemDisplayed(), Is.True, "Cart item was not added in the cart");
        }

        [Test]
        public void TestPageTitle()
        {
            Assert.That(inventoryPage.IsInventoryPageLoaded, Is.True, "Inventory page not loaded correctly.");
        }

    }
}
