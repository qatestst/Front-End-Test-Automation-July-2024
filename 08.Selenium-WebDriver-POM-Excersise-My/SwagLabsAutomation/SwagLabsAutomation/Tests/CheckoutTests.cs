using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwagLabsAutomation.Tests
{
    public class CheckoutTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Login("standard_user", "secret_sauce");
            inventoryPage.AddToCartByIndex(1);
            inventoryPage.ClickCartLink();
            cartPage.ClickCheckout();
        }

        [Test]
        public void TestCheckoutPageLoaded()
        {
            Assert.True(checkoutPage.IsPageLoaded(), "Checkout page not loaded");
        }

        [Test]
        public void TestContinueToNextStep()
        {
            checkoutPage.EnterFirstName("FirstName");
            checkoutPage.EnterLastName("LastName");
            checkoutPage.EnterPostalCode("4556");
            checkoutPage.ClickContinue();

            Assert.That(driver.Url.Contains("checkout-step-two"), Is.True, "Not navigated to the correct checkout page.");
        }

        [Test]
        public void TestCompleteOrder()
        {
            checkoutPage.EnterFirstName("FirstName");
            checkoutPage.EnterLastName("LastName");
            checkoutPage.EnterPostalCode("4556");
            checkoutPage.ClickContinue();
            checkoutPage.ClickFinish();

            Assert.True(checkoutPage.IsCheckoutComplete(), "Checkout was not completed.");
        }
    }
}
