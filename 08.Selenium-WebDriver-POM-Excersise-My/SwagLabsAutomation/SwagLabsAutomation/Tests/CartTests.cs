using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Tests
{
    public class CartTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
        }

        [Test]
        public void TestCartItemsDisplayed()
        {
            Assert.True(cartPage.IsCartItemDisplayed(), "There were no products in the cart.");
        }

        [Test]
        public void TestClickCheckout()
        {
            cartPage.ClickCheckout();
            Assert.That(checkoutPage.IsPageLoaded(), Is.True, "Not navigated to the checkout page");
        }


    }
}
